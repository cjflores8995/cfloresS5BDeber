using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfloresS5B.Models
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;

        public string StatusMessage {  get; set; }

        private void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
            Init();
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;

            try
            {
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre requerido");

                Persona persona = new Persona() { Name = nombre };
                result = conn.Insert(persona);

                StatusMessage = string.Format($"{result} record(s) added (Nombre: {nombre})");

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format($"Failed to add {nombre}. Error: {ex.Message})");
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                var lista = conn.Table<Persona>().ToList();
                return lista;

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format($"Failed to retrieve data. {ex.Message})");
            }

            return new List<Persona>();
        }

        public void UpdatePerson(Persona persona)
        {
            try
            {
                int result = conn.Update(persona);
                StatusMessage = $"{result} persona actualizada: Id={persona.Id}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al actualizar: {ex.Message}";
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                var persona = conn.Find<Persona>(id);
                if (persona != null)
                {
                    int result = conn.Delete(persona);
                    StatusMessage = $"{result} Persona eliminada: Id={id}";
                }
                else
                {
                    StatusMessage = $"No se encontró registro con Id={id}";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar: {ex.Message}";
            }
        }
    }
}
