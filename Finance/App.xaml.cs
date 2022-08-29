using System.Globalization;

namespace Finance;

public partial class App : Application
{
	public App()
	{
        InitializeComponent();

        // Set the language to test the application, otherwise comment out the next line.
        //CultureInfo.CurrentCulture = new CultureInfo("nl-BE");

        MainPage = new AppShell();
	}
}
