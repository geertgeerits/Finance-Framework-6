﻿// Program .....: Finance.sln
// Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
// Copyright ...: (C) 1992-2022
// Version .....: 3.0.48 Beta
// Date ........: 2022-09-27 (YYYY-MM-DD)
// Language ....: Microsoft Visual Studio 2022: .NET MAUI C# 10.0
// Description .: Financial calculations

using System.Globalization;
using Finance.Resources;

namespace Finance;

public partial class MainPage : ContentPage
{
    // Global variables for all pages part of Finance.
    public static string cTheme;
    public static bool bDateFormatSystem;
    public static string cSysDateFormat;
    public static string cDateFormat;
    public static string cNumDecimalSeparator;
    public static string cRoundNumber;
    public static string cISOCurrencyCode;
    public static string cKeyboard;
    public static string cLanguage;
    public static string cPageFormat;

    public static string cErrorTitleText;
    public static string cButtonCloseText;

    // Local variables.
    private string cCopyright;
    private string cLicenseTitle;
    private string cLicenseText;
    private string cAgree;
    private string cDisagree;
    private bool bLicense;

    public MainPage()
    {
        InitializeComponent();

        // Get the saved settings.License
        cTheme = Preferences.Get("SettingTheme", "System");
        bDateFormatSystem = Preferences.Get("SettingDateFormatSystem", true);
        cPageFormat = Preferences.Get("SettingPageFormat", "");
        cRoundNumber = Preferences.Get("SettingRoundNumber", "AwayFromZero");
        cKeyboard = Preferences.Get("SettingKeyboard", "Numeric");
        cLanguage = Preferences.Get("SettingLanguage", "");
        bLicense = Preferences.Get("SettingLicense", false);

#if WINDOWS
        // Set the settings default values because of a BUG in the Windows app they have NULL values !!!
        if (cTheme == null)
        {
            cTheme = "System";
        }

        if (cPageFormat == null)
        {
            cPageFormat = "";
        }

        if (cRoundNumber == null)
        {
            cRoundNumber = "AwayFromZero";
        }

        if (cKeyboard == null)
        {
            cKeyboard = "Numeric";
        }

        if (cLanguage == null)
        {
            cLanguage = "";
        }
#endif

        // Set the theme.
        if (cTheme == "Light")
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        else if (cTheme == "Dark")
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Unspecified;
        }

        // Get the system date format and set the date format.
        cSysDateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        
        if (bDateFormatSystem == true)
        {
            cDateFormat = cSysDateFormat;
        }
        else
        {
            cDateFormat = "yyyy-MM-dd";
        }
        //App.Current.MainPage.DisplayAlert("cDateFormat", cDateFormat, "OK");

        // Get the number decimal separator.
        cNumDecimalSeparator = Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        // Get the system ISO currency code.
        string cCountry = Thread.CurrentThread.CurrentCulture.Name.Substring(3, 2);       
        
        RegionInfo myRegInfo = new RegionInfo(cCountry);
        cISOCurrencyCode = myRegInfo.ISOCurrencySymbol;

        // Set the page format.
        if (cPageFormat == "")
        {
            string cCountryCodePageFormatLetter = "CA;CL;CO;CR;DO;GT;MX;PA;PH;US";

            if (cCountryCodePageFormatLetter.Contains(cCountry))
            {
                cPageFormat = "Letter";
            }
            else
            {
                cPageFormat = "A4";
            }
        }

        // Set the rounding system of numbers.
        if (cRoundNumber == "")
        {
            cRoundNumber = "AwayFromZero";
        }

        // Get and set the system OS user language.
        if (cLanguage == "")
        {
            cLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }

        SetTextLanguage();

        // Show license.  Does not works in Windows !!!
        //ShowLicense(bLicense);
    }

    // Buttons clicked events.
    private async void OnPageInterestEffectiveClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageInterestEffective());
    }

    private async void OnPageInterestEffectiveBEClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageInterestEffectiveBE());
    }

    private async void OnPageInterestAnnualClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageInterestAnnual());
    }

    private async void OnPageInterestMonthDayClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageInterestMonthDay());
    }

    private async void OnPageInterestPayDiscountClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageInterestPayDiscount());
    }

    private async void OnPageLoanDetailClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageLoanDetail());
    }

    private async void OnPageVATCalculationClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageVATCalculation());
    }

    private async void OnPageAmountGrossOfNetClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageAmountGrossOfNet());
    }

    private async void OnPageInvestmentReturnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageInvestmentReturn());
    }

    private async void OnPageDifferenceNumbersClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageDifferenceNumbers());
    }

    private async void OnPageDifferenceDatesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageDifferenceDates());
    }

    private async void OnPageSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageSettings());
    }

    private async void OnPageAboutClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageAbout());
    }
    
        // Replace decimal point with decimal comma.
    public static string ReplaceDecimalPointComma(string cNumber)
    {
        if (cNumDecimalSeparator == "," && cNumber.Contains('.'))
        {
            cNumber = cNumber.Replace(".", ",");
        }
        else if (cNumDecimalSeparator == "." && cNumber.Contains(','))
        {
            cNumber = cNumber.Replace(",", ".");
        }

        return cNumber;
    }

    // Close the keyboard.
    public static void CloseKeyboard(object sender, EventArgs e)
    {
        try
        {
            var entry = (Entry)sender;
            entry.IsEnabled = false;
            entry.IsEnabled = true;
        }
        catch (Exception)
        {
            return;
        }
    }

    // Rounding numbers.
    // Round away from zero: MidpointRounding.AwayFromZero = 1-4 down ; 5-9 up
    // Round half to even or banker's rounding: MidpointRounding.ToEven
    //       Value      Default    ToEven     AwayFromZero    ToZero
    //       12.0       12         12         12              12
    //       12.1       12         12         12              12
    //       12.2       12         12         12              12
    //       12.3       12         12         12              12
    //       12.4       12         12         12              12
    //       12.5       12         12         13              12
    //       12.6       13         13         13              12
    //       12.7       13         13         13              12
    //       12.8       13         13         13              12
    //       12.9       13         13         13              12
    //       13.0       13         13         13              13

    // Format specifier: "F" = 1234.56 or 1234,56 ; "N" = 1,234.56 or 1.234,56.

    // Rounding and formatting double number to # decimals returning number as value and as string.
    public static string RoundDoubleToNumDecimals(ref double nNumber, int nNumDec, string cFormatSpecifier)
    {
        if (cRoundNumber == "AwayFromZero")
        {
            nNumber = Math.Round(nNumber, nNumDec, MidpointRounding.AwayFromZero);
        }
        else if (cRoundNumber == "ToEven")
        {
            nNumber = Math.Round(nNumber, nNumDec, MidpointRounding.ToEven);
        }
        
        string cNumFormat = cFormatSpecifier + nNumDec.ToString();
        return nNumber.ToString(cNumFormat);
    }

    // Rounding and formatting decimal number to # decimals returning number as value and as string.
    public static string RoundDecimalToNumDecimals(ref decimal nNumber, int nNumDec, string cFormatSpecifier)
    {
        if (cRoundNumber == "AwayFromZero")
        {
            nNumber = Math.Round(nNumber, nNumDec, MidpointRounding.AwayFromZero);
        }
        else if (cRoundNumber == "ToEven")
        {
            nNumber = Math.Round(nNumber, nNumDec, MidpointRounding.ToEven);
        }

        string cNumFormat = cFormatSpecifier + nNumDec.ToString();
        return nNumber.ToString(cNumFormat);
    }

    // Set language.
    void OnPickerLanguageChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            //string cSelected = picker.Items[selectedIndex];
            //App.Current.MainPage.DisplayAlert("cSelected", cSelected, "OK");  // For testing

            switch (selectedIndex)
            {
                // German (Deutsch).
                case 0:
                    MainPage.cLanguage = "de";
                    break;

                // Spanish (Español).
                case 2:
                    MainPage.cLanguage = "es";
                    break;

                // French (Français).
                case 3:
                    MainPage.cLanguage = "fr";
                    break;

                // Italian (Italiano).
                case 4:
                    MainPage.cLanguage = "it";
                    break;

                // Dutch (Nederlands).
                case 5:
                    MainPage.cLanguage = "nl";
                    break;

                // Portuguese (Português).
                case 6:
                    MainPage.cLanguage = "pt";
                    break;

                // English.
                default:
                    MainPage.cLanguage = "en";
                    break;
            }
            //App.Current.MainPage.DisplayAlert("cLanguage", cLanguage, "OK");  // For testing
            SetTextLanguage();
        }
    }

    // Put text in the chosen language in the controls.
    private void SetTextLanguage()
    {
        // Set the CurrentUICulture.
        //cLanguage = "es";
        //App.Current.MainPage.DisplayAlert("cLanguage", cLanguage, "OK");  // For testing.

        switch (cLanguage)
        {
            // German (Deutsch).
            case "de":
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de");
                pickerLanguage.SelectedIndex = 0;
                break;

            // Spanish (Español).
            case "es":
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es");
                pickerLanguage.SelectedIndex = 2;
                break;

            // French (Français).
            case "fr":
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr");
                pickerLanguage.SelectedIndex = 3;
                break;

            // Italian (Italiano).
            case "it":
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("it");
                pickerLanguage.SelectedIndex = 4;
                break;

            // Dutch (Nederlands).
            case "nl":
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("nl");
                pickerLanguage.SelectedIndex = 5;
                break;

            // Portuguese (Português).
            case "pt":
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("pt");
                pickerLanguage.SelectedIndex = 6;
                break;

            // English.
            default:
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                pickerLanguage.SelectedIndex = 1;
                break;
        }

        btnInterestEffective.Text = FinLang.FinanceInterestEffective_Text;
        btnInterestEffectiveBE.Text = FinLang.FinanceInterestEffectiveBE_Text;
        btnInterestPayDiscount.Text = FinLang.FinanceInterestPayDiscount_Text;
        btnInterestMonthDay.Text = FinLang.FinanceInterestMonthDay_Text;
        btnInterestAnnual.Text = FinLang.FinanceInterestAnnual_Text;
        btnLoanDetail.Text = FinLang.FinanceLoanDetail_Text;
        btnVATCalculation.Text = FinLang.FinanceVATCalculation_Text;
        btnAmountGrossOfNet.Text = FinLang.FinanceAmountGrossOfNet_Text;
        btnInvestmentReturn.Text = FinLang.FinanceInvestmentReturn_Text;
        btnDifferenceNumbers.Text = FinLang.FinanceDifferenceNumbers_Text;
        btnDifferenceDates.Text = FinLang.FinanceDifferenceDates_Text;

        cErrorTitleText = FinLang.ErrorTitle_Text;
        cButtonCloseText = FinLang.ButtonClose_Text;

        cCopyright = FinLang.Copyright_Text + " © 1992-2022 Geert Geerits";
        cLicenseTitle = FinLang.LicenseTitle_Text;
        cLicenseText = FinLang.License_Text;
        cAgree = FinLang.Agree_Text;
        cDisagree = FinLang.Disagree_Text;

        //App.Current.MainPage.DisplayAlert(cErrorTitleText, cLanguage, cButtonCloseText);  // For testing.
    }

    // Show license.
    //// Does not work in Windows!!!  
    //private async void ShowLicense(bool bLicense)
    //{
    //    if (bLicense == false)
    //    {
    //        bool bAnswer = await Application.Current.MainPage.DisplayAlert(cLicenseTitle, "Finance" + "\n" + cCopyright + "\n\n" + cLicenseText, cAgree, cDisagree);

    //        if (bAnswer)
    //        {
    //            Preferences.Default.Set("SettingLicense", true);
    //        }
    //        else
    //        {
    //            Application.Current.Quit();
    //        }
    //    }
    //}

    // Show license for Android and Windows = Workaround !!!
    // Solution is using the Loaded event of the MainPage.xaml.
    private async void OnLoad(object sender, EventArgs e)
    {
        if (bLicense == false)
        {
            bool bAnswer = await Application.Current.MainPage.DisplayAlert(cLicenseTitle, "Finance" + "\n" + cCopyright + "\n\n" + cLicenseText, cAgree, cDisagree);

            if (bAnswer)
            {
                Preferences.Default.Set("SettingLicense", true);
            }
            else
            {
                Application.Current.Quit();
            }
        }
    }
}
