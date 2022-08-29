﻿using Finance.Resources;

namespace Finance;
public partial class PageDifferenceDates : ContentPage
{
    public PageDifferenceDates()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            DisplayAlert("InitializeComponent", ex.Message, MainPage.cButtonCloseText);
            return;
        }

        // Put text in the chosen language in the controls.
        lblTitle.Text = FinLang.DifferenceDatesForm_Text;

        lblExplanation.Text = FinLang.DifferenceExplanationDates_Text;
        lblDate1.Text = FinLang.Date1_Text;
        lblDate2.Text = FinLang.Date2_Text;
        lblDateDifferenceDays.Text = FinLang.DateDifferenceDays_Text;
        lblDateDifferenceHours.Text = FinLang.DateDifferenceHours_Text;
        lblDateDifferenceMinutes.Text = FinLang.DateDifferenceMinutes_Text;
        lblDateDifferenceSeconds.Text = FinLang.DateDifferenceSeconds_Text;

        btnCalculate.Text = FinLang.Calculate_Text;
        btnReset.Text = FinLang.Reset_Text;

        // Set the date properties for the DatePicker.
        dtpDate1.MinimumDate = new DateTime(1583, 1, 1);
        dtpDate1.MaximumDate = new DateTime(3999, 12, 31);
        dtpDate2.MinimumDate = new DateTime(1583, 1, 1);
        dtpDate2.MaximumDate = new DateTime(3999, 12, 31);

        dtpDate1.Format = MainPage.cDateFormat;
        dtpDate2.Format = MainPage.cDateFormat;

        // Set focus to the first entry field.
        dtpDate1.Focus();
    }

    // Clear result field if the date have changed.
    private void DatePickerDateSelected(object sender, EventArgs e)
    {
        txtDateDifferenceDays.Text = "";
        txtDateDifferenceHours.Text = "";
        txtDateDifferenceMinutes.Text = "";
        txtDateDifferenceSeconds.Text = "";
    }

    // Go to the next field when the return key have been pressed.
    private void GoToNextField(object sender, EventArgs e)
    {
        if (sender == dtpDate1)
        {
            dtpDate2.Focus();
        }
    }

    // Calculate the result.
    private void CalculateResult(object sender, EventArgs e)
    {
        // Calculate the difference.
        {
            decimal nDateDifference = (dtpDate2.Date - dtpDate1.Date).Days;
            txtDateDifferenceDays.Text = MainPage.RoundDecimalToNumDecimals(ref nDateDifference, 0, "N");
            
            nDateDifference *= 24;
            txtDateDifferenceHours.Text = MainPage.RoundDecimalToNumDecimals(ref nDateDifference, 0, "N");
            
            nDateDifference *= 60;
            txtDateDifferenceMinutes.Text = MainPage.RoundDecimalToNumDecimals(ref nDateDifference, 0, "N");
            
            nDateDifference *= 60;
            txtDateDifferenceSeconds.Text = MainPage.RoundDecimalToNumDecimals(ref nDateDifference, 0, "N");
        }

        // Set focus.
        btnCalculate.Focus();
    }

    // Reset the entry fields.
    private void ResetEntryFields(object sender, EventArgs e)
    {
        dtpDate1.Date = DateTime.Today;
        dtpDate2.Date = DateTime.Today;
        txtDateDifferenceDays.Text = "";
        txtDateDifferenceHours.Text = "";
        txtDateDifferenceMinutes.Text = "";
        txtDateDifferenceSeconds.Text = "";

        //dtpDate1.Focus();  //Let the date picker to pop up in Android
    }
}
