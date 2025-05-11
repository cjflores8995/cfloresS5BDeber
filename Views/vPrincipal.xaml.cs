using cfloresS5B.Models;

namespace cfloresS5B.Views;

public partial class vPrincipal : ContentPage
{
	public vPrincipal()
	{
		InitializeComponent();
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";

        App.personRepo.AddNewPerson(txtNombre.Text);
        statusMessage.Text = App.personRepo.StatusMessage;
    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";

        List<Persona> people = App.personRepo.GetAllPeople();
        listaPersona.ItemsSource = people;
    }

    private async void btnEditar_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn?.CommandParameter is Persona persona)
        {
            string nuevoNombre = await DisplayPromptAsync(
                "Editar nombre",
                "Ingrese el nuevo nombre:",
                initialValue: persona.Name);
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                persona.Name = nuevoNombre.Trim();
                App.personRepo.UpdatePerson(persona);
                statusMessage.Text = App.personRepo.StatusMessage;
                listaPersona.ItemsSource = App.personRepo.GetAllPeople();
            }
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn?.CommandParameter is int id)
        {
            bool confirm = await DisplayAlert(
                "Confirmar eliminación",
                $"¿Eliminar registro Id={id}?",
                "Sí", "No");
            if (confirm)
            {
                App.personRepo.DeletePerson(id);
                statusMessage.Text = App.personRepo.StatusMessage;
                listaPersona.ItemsSource = App.personRepo.GetAllPeople();
            }
        }
    }
}