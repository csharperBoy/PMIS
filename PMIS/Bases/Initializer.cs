using PMIS.Models;
using PMIS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMIS.Bases
{
    internal class Initializer : IInitializer
    {
        public void Initialize()
        {
            // Check if the database is already seeded
            new DbInitializer().Initialize(); // Initialize database.
            // Seed other entities similarly
        }

        private class DbInitializer : IDbInitializer
        {
            private PmisContext? context;
            public void Initialize()
            {
                ((IDbInitializer)this).Create(); // Create database if doesn't exist.
                ((IDbInitializer)this).SetData(); // Set data in database if doesn't exist.
            }

            void IDbInitializer.Create()
            {
                try
                {
                    throw new NotImplementedException("عدم پیاده سازی تابع ساخت اولیه پایگاه داده از روی مدل‌ها!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            void IDbInitializer.SetData()
            {
                context = new PmisContext();

                #region SetAllLookUps
                setAllLookUps(context);
                #endregion

                #region SetBaseUsers
                SetBaseUsers(context);
                #endregion
            }

            private void setAllLookUps(PmisContext context)
            {
                context.Database.EnsureCreated();
                if (!context.LookUps.Any())
                {
                    LookUp lookup = new LookUp();
                    List<LookUpDestination> lookUpDestination = new List<LookUpDestination>();
                    List<LookUpValue> lookUpValue = new List<LookUpValue>();

                    #region Form
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpForm", Title = "فرم‌های دستی" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "Indicator", ColumnName = "FkLkpFormID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Other", Display = "سایر", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Inventory", Display = "موجودی", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Bourse", Display = "بورس", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "PromotionOffer", Display = "پیشنهاد انگیزشی", OrderNum = 4 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region Manuality
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpManuality", Title = "پارامترهای دستی" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "Indicator", ColumnName = "FkLkpManualityID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Nothing", Display = "هیچکدام", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Foresight", Display = "پیش‌بینی", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Target", Display = "هدف", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Performance", Display = "عملکرد", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "FT", Display = "پیش‌بینی/هدف", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "FP", Display = "پیش‌بینی/عملکرد", OrderNum = 6 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "TP", Display = "هدف/عملکرد", OrderNum = 7 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "FTP", Display = "پیش‌بینی/هدف/عملکرد", OrderNum = 8 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region Unit
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpUnit", Title = "واحدهای سازمانی" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "Indicator", ColumnName = "FkLkpUnitID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Nothing", Display = "هیچکدام", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Management", Display = "حوزه مدیریت", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "TechnologyEngineering", Display = "فناوری و مهندسی", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "HumanCapital", Display = "سرمایه‌های انسانی", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "EconomicFinancial", Display = "اقتصادی و مالی", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Commerce", Display = "بازرگانی", OrderNum = 6 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Exploitation", Display = "بهره‌برداری", OrderNum = 7 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Public", Display = "عمومی", OrderNum = 8 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region Period
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpPeriod", Title = "نوع بازه‌های زمانی" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "Indicator", ColumnName = "FkLkpPeriodID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Annually", Display = "سالانه", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Biannual", Display = "نیمسالانه", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Seasonal", Display = "فصلی", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Monthly", Display = "ماهانه", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Weekly", Display = "هفتگی", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Dayly", Display = "روزانه", OrderNum = 6 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Shiftly", Display = "شیفتی", OrderNum = 7 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Hourly", Display = "ساعتی", OrderNum = 8 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region Measure
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpMeasure", Title = "واحدهای اندازه‌گیری" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "Indicator", ColumnName = "FkLkpMeasureID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Nothing", Display = "هیچکدام", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Percentage", Display = "درصد", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Rial", Display = "ریال", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Euro", Display = "یورو", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Dollar", Display = "دلار", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Dirham", Display = "درهم", OrderNum = 6 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Milligram", Display = "میلی‌گرم", OrderNum = 7 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Gram", Display = "گرم", OrderNum = 8 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Kilogram", Display = "کیلوگرم", OrderNum = 9 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Ton", Display = "تن", OrderNum = 10 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Meter", Display = "متر", OrderNum = 11 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Kilometer", Display = "کیلومتر", OrderNum = 12 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Squaremeter", Display = "مترمربع", OrderNum = 13 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Cubicmeter", Display = "مترمکعب", OrderNum = 14 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Litre", Display = "لیتر", OrderNum = 15 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "GramPerSquaremeter", Display = "گرم بر مترمربع", OrderNum = 16 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region Desirability
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpDesirability", Title = "مطلوبیت شاخص‌ها" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "Indicator", ColumnName = "FkLkpDesirabilityID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Nothing", Display = "هیچکدام", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Ascending", Display = "صعودی", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Descending", Display = "نزولی", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Stable", Display = "پایدار", OrderNum = 4 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region ValueType
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpValueType", Title = "نوع مقدار" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "IndicatorValue", ColumnName = "FkLkpValueTypeID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Nothing", Display = "هیچکدام", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Foresight", Display = "پیش‌بینی", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Target", Display = "هدف", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Performance", Display = "عملکرد", OrderNum = 4 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region Shift
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpShift", Title = "شیفت کاری خطوط" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "IndicatorValue", ColumnName = "FkLkpShiftID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "0", Display = "هیچ کدام", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "1", Display = "شیفت 1", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "2", Display = "شیفت 2", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "3", Display = "شیفت 3", OrderNum = 4 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region ClaimUserOnIndicator
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpClaimUserOnIndicator", Title = "ادعاهای کاربر" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "ClaimUserOnIndicator", ColumnName = "FkLkpClaimUserOnIndicatorID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Owner", Display = "مالک", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "ResponsibleForesight", Display = "مسئول پیش‌بینی", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "ResponsibleTarget", Display = "مسئول هدف", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "ResponsiblePerformance", Display = "مسئول عملکرد", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "MonitorForesight", Display = "ویرایشگر پیش‌بینی", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "MonitorTarget", Display = "ویرایشگر هدف", OrderNum = 6 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "MonitorPerformance", Display = "ویرایشگر عملکرد", OrderNum = 7 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "WriterForesight", Display = "نگارنده پیش‌بینی", OrderNum = 8 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "WriterTarget", Display = "نگارنده هدف", OrderNum = 9 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "WriterPerformance", Display = "نگارنده عملکرد", OrderNum = 10 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region WorkCalendar
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpWorkCalendar", Title = "تقویم کاری" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "User", ColumnName = "FkLkpWorkCalendarID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Nothing", Display = "هیچکدام", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "R", Display = "روزکار", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "A", Display = "تیم A", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "B", Display = "تیم B", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "C", Display = "تیم C", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "D", Display = "تیم D", OrderNum = 6 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region CategoryType
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpCategoryType", Title = "نوع‌های دسته‌بندی" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "IndicatorCategory", ColumnName = "FkLkpCategoryTypeID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "EndUser", Display = "دسته‌بندی کاربر نهایی", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Developer", Display = "دسته‌بندی توسعه‌دهنده", OrderNum = 2 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region CategoryMasterEndUser
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpCategoryMasterEndUser", Title = "دسته‌بندی‌های کاربر نهایی" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "IndicatorCategory", ColumnName = "FkLkpCategoryMasterID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "SYS", Display = "نظام‌های مدیریتی", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "STD", Display = "استانداردهای مدیریتی", OrderNum = 2 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region CategoryDetailEndUserSYS
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpCategoryDetailEndUserSYS", Title = "مقادیر نظام‌های مدیریتی" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "IndicatorCategory", ColumnName = "FkLkpCategoryDetailID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "RiskManagement", Display = "مدیریت ریسک", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "ProcessManagement", Display = "مدیریت فرآیندی", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "TaskLevelStrategicManagement", Display = "مدیریت استراتژیک سطح وظیفه‌ای", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "MacroLevelStrategicManagement", Display = "مدیریت استراتژیک سطح کلان", OrderNum = 4 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region CategoryDetailEndUserSTD
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpCategoryDetailEndUserSTD", Title = "مقادیر استاندارد‌های مدیریتی" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "IndicatorCategory", ColumnName = "FkLkpCategoryDetailID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Quality", Display = "کیفیت ISO9001", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Environmental", Display = "زیست‌محیطی ISO14001", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "SafetyAndHealth", Display = "ایمنی و بهداشت ISO45001", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Energy", Display = "انرژی ISO50001", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Laboratory", Display = "آزمایشگاه ISO17025", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Education", Display = "آموزش ISO10015", OrderNum = 6 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "CustomerComplaints", Display = "شکایت مشتریان ISO10002", OrderNum = 7 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "CustomerSatisfaction", Display = "رضایت مشتریان ISO10004", OrderNum = 8 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Action", Display = "اقدام", OrderNum = 9 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Other", Display = "سایر", OrderNum = 10 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion
                }
            }

            private void SetBaseUsers(PmisContext context)
            {
                context.Database.EnsureCreated();
                if (!context.Users.Any())
                {
                    List<Models.User> user = new List<Models.User>();
                    #region PrimaryUsers
                    //add user
                    //------------------------------------
                    user.Add(new Models.User { UserName = "Admin", PasswordHash = Hasher.HasherHMACSHA512.Hash("Admin+123"), FullName = "فرمانروا", FkLkpWorkCalendarId = context.LookUpValues.Where(x => x.Value == "Nothing" && x.FkLookUp.Code == "LkpWorkCalendar").First().Id });
                    context.Users.AddRange(user);
                    context.SaveChanges();
                    //------------------------------------
                    #endregion
                }
            }
        }
    }
}