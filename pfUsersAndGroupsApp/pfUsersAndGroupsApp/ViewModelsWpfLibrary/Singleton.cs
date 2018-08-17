using ViewModelsWpfLibrary.ViewModels;

namespace ViewModelsWpfLibrary
{
    public sealed class Singleton
    {
        private static Singleton SingletonInstance;

        public GeneralViewModel GeneralViewModel { get; set; }

        private Singleton()
        {
            GeneralViewModel = new GeneralViewModel();
        }

        public static Singleton GetInstance()
        {
            if (SingletonInstance == null)
            {
                SingletonInstance = new Singleton();
            }

            return SingletonInstance;
        }
    }
}
