using cfloresS5B.Models;
using cfloresS5B.Views;

namespace cfloresS5B
{
    public partial class App : Application
    {
        public static PersonRepository personRepo {  get; set; }
        public App(PersonRepository personRepository)
        {
            InitializeComponent();
            personRepo = personRepository;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new vPrincipal());
        }
    }
}