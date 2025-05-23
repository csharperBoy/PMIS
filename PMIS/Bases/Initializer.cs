﻿using PMIS.Models;
using PMIS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
                    using (var dbContext = new PmisContext())
                    {
                        dbContext.Database.EnsureCreated();
                        //dbContext.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("عملیات ساخت پایگاه داده موفقیت‌آمیز نبود: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            void IDbInitializer.SetData()
            {
                try
                {
                    context = new PmisContext();

                    #region setBaseLookUps
                    setBaseLookUps(context);
                    #endregion

                    #region SetBaseUsers
                    SetBaseUsers(context);
                    #endregion

                    #region SetBaseClaims
                    SetBaseClaims(context);
                    #endregion

                    #region setBaseCategories
                    setBaseCategories(context);
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show("عملیات مقداردهی اولیه اطلاعات پایه موفقیت‌آمیز نبود: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void setBaseLookUps(PmisContext context)
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
                    lookup = new LookUp { Code = "LkpForm", Title = "نام فرم" };
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
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Assurance", Display = "اختصاصی تضمین", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Assurance", Display = "سود و زیان", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Assurance", Display = "منابع و مصارف", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "BanksInventory", Display = "موجودی بانکها", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Bourse", Display = "بورس", OrderNum = 6 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "PromotionOffers", Display = "پیشنهادات انگیزشی", OrderNum = 7 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form01", Display = "فرم 1", OrderNum = 8 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form02", Display = "فرم 2", OrderNum = 9 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form03", Display = "فرم 3", OrderNum = 10 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form04", Display = "فرم 4", OrderNum = 11 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form05", Display = "فرم 5", OrderNum = 12 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form06", Display = "فرم 6", OrderNum = 13 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form07", Display = "فرم 7", OrderNum = 14 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form08", Display = "فرم 8", OrderNum = 15 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form09", Display = "فرم 9", OrderNum = 16 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form10", Display = "فرم 10", OrderNum = 17 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form11", Display = "فرم 11", OrderNum = 18 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form12", Display = "فرم 12", OrderNum = 19 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Form13", Display = "فرم 13", OrderNum = 20 });
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
                    lookup = new LookUp { Code = "LkpUnit", Title = "شاخص یا اقدام" };
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
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Indicator", Display = "شاخص", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Action", Display = "اقدام", OrderNum = 2 });
                   
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region Period
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpPeriod", Title = "دوره پایش" };
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
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Counting:Percentage", Display = "درصد", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Counting:Quantity", Display = "عدد", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Counting:Man", Display = "نفر", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Time:Year", Display = "سال", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Time:Month", Display = "ماه", OrderNum = 6 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Time:Week", Display = "هفته", OrderNum = 7 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Time:Day", Display = "روز", OrderNum = 8 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Time:Hour", Display = "ساعت", OrderNum = 9 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Time:Minute", Display = "دقیقه", OrderNum = 10 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Time:Second", Display = "ثانیه", OrderNum = 11 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Time:MilliSecond", Display = "میلی‌ثانیه", OrderNum = 12 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Action:Man.Hour", Display = "نفر ساعت", OrderNum = 13 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Mass:Ton", Display = "تن", OrderNum = 14 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Mass:KiloGram", Display = "کیلوگرم", OrderNum = 15 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Mass:Gram", Display = "گرم", OrderNum = 16 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Mass:MilliGram", Display = "میلی‌گرم", OrderNum = 17 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Length:KiloMeter", Display = "کیلومتر", OrderNum = 18 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Length:Meter", Display = "متر", OrderNum = 19 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Length:CentiMeter", Display = "سانتی‌متر", OrderNum = 20 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Length:MilliMeter", Display = "میلی‌متر", OrderNum = 21 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Space:SquareMeter", Display = "مترمربع", OrderNum = 22 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Space:SquareCentiMeter", Display = "سانتی‌مترمربع", OrderNum = 23 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Volume:CubicMeter", Display = "مترمکعب", OrderNum = 24 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Volume:CubicCentiMeter", Display = "سانتی‌مترمکعب", OrderNum = 25 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Volume:Litre", Display = "لیتر", OrderNum = 26 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Angle:Radian", Display = "رادیان", OrderNum = 27 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Angle:Degree", Display = "درجه", OrderNum = 28 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Angle:Grad", Display = "گراد", OrderNum = 29 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Temperature:Kelvin", Display = "کلوین", OrderNum = 30 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Temperature:Celsius", Display = "سلسیوس", OrderNum = 31 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Temperature:Fahrenheit", Display = "فارنهایت", OrderNum = 32 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Power:KiloWatt", Display = "کیلووات", OrderNum = 33 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Power:Watt", Display = "وات", OrderNum = 34 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Energy:KiloWatt.Hour", Display = "کیلووات ساعت", OrderNum = 35 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Energy:Watt.Hour", Display = "وات ساعت", OrderNum = 36 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Density:KiloGram/CubicMeter", Display = "کیلوگرم بر مترمکعب", OrderNum = 37 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Density:Gram/CubicCentiMeter", Display = "گرم بر سانتی‌مترمکعب", OrderNum = 38 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Density:PPM", Display = "قسمت در میلیون", OrderNum = 39 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:Rial", Display = "ریال", OrderNum = 40 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:KRial", Display = "هزار ریال", OrderNum = 41 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:MRial", Display = "میلیون ریال", OrderNum = 42 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:GRial", Display = "میلیارد ریال", OrderNum = 43 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:TRial", Display = "هزار میلیارد ریال", OrderNum = 44 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:Euro", Display = "یورو", OrderNum = 45 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:KEuro", Display = "هزار یورو", OrderNum = 46 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:MEuro", Display = "میلیون یورو", OrderNum = 47 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:GEuro", Display = "میلیارد یورو", OrderNum = 48 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:Dollar", Display = "دلار", OrderNum = 49 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:KDollar", Display = "هزار دلار", OrderNum = 50 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:MDollar", Display = "میلیون دلار", OrderNum = 51 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:GDollar", Display = "میلیارد دلار", OrderNum = 52 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:Dirham", Display = "درهم", OrderNum = 53 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:KDirham", Display = "هزار درهم", OrderNum = 54 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:MDirham", Display = "میلیون درهم", OrderNum = 55 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Money:GDirham", Display = "میلیارد درهم", OrderNum = 56 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Man.Hour/PTon", Display = "نفر ساعت بر تن تولید", OrderNum = 57 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Man.Hour/PTonRecycle", Display = "نفر ساعت بر تن اسیدبازیافتی", OrderNum = 58 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Ton/Hour", Display = "تن بر ساعت", OrderNum = 59 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Ton/Minute", Display = "تن بر دقیقه", OrderNum = 60 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KiloGram/Ton", Display = "کیلوگرم بر تن", OrderNum = 61 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KiloGram/PTon", Display = "کیلوگرم بر تن تولید", OrderNum = 62 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KiloGram/PTonRecycle", Display = "کیلوگرم بر تن تولید اسیدبازیافتی", OrderNum = 63 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Gram/SquareMeter", Display = "گرم بر مترمربع", OrderNum = 64 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "MilliGram/Litre", Display = "میلی‌گرم بر لیتر", OrderNum = 65 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Meter/Ton", Display = "متر بر تن", OrderNum = 66 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "MilliMeter/Ton", Display = "میلی‌متر بر تن", OrderNum = 67 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "CubicMeter/Litre", Display = "مترمکعب بر لیتر", OrderNum = 68 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "CubicMeter/PTon", Display = "مترمکعب بر تن تولید", OrderNum = 69 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "CubicMeter/PTonRecycle", Display = "مترمکعب بر تن تولید اسیدبازیافتی", OrderNum = 70 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Litre/Hour", Display = "لیتر بر ساعت", OrderNum = 71 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Litre/Second", Display = "لیتر بر ثانیه", OrderNum = 72 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Litre/PTon", Display = "لیتر بر تن تولید", OrderNum = 73 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Litre/PTonRecycle", Display = "لیتر بر تن اسید بازیافتی", OrderNum = 74 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KiloWatt.Hour/PTon", Display = "کیلووات ساعت بر تن تولید", OrderNum = 75 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KiloWatt.Hour/PTonRecycle", Display = "کیلووات ساعت بر تن اسیدبازیافتی", OrderNum = 76 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Rial/Ton", Display = "ریال بر تن", OrderNum = 77 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Rial/PTon", Display = "ریال بر تن تولید", OrderNum = 78 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Rial/PTonRecycle", Display = "ریال بر تن تولید اسیدبازیافتی", OrderNum = 79 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KRial/Ton", Display = "هزار ریال بر تن", OrderNum = 80 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KRial/PTon", Display = "هزار ریال بر تن تولید", OrderNum = 81 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KRial/PTonRecycle", Display = "هزار ریال بر تن تولید اسیدبازیافتی", OrderNum = 82 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "MRial/Ton", Display = "میلیون ریال بر تن", OrderNum = 83 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Mial/PTon", Display = "میلیون ریال بر تن تولید", OrderNum = 84 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Mial/PTonRecycle", Display = "میلیون ریال بر تن تولید اسیدبازیافتی", OrderNum = 85 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Dollar/Ton", Display = "دلار بر تن", OrderNum = 86 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Dollar/PTon", Display = "دلار بر تن تولید", OrderNum = 87 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "Dollar/PTonRecycle", Display = "دلار بر تن تولید اسیدبازیافتی", OrderNum = 88 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KDollar/Ton", Display = "هزار دلار بر تن", OrderNum = 89 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KDollar/PTon", Display = "هزار دلار بر تن تولید", OrderNum = 90 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "KDollar/PTonRecycle", Display = "هزار دلار بر تن تولید اسیدبازیافتی", OrderNum = 91 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region Desirability
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpDesirability", Title = "روند مطلوبیت" };
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
                    lookup = new LookUp { Code = "LkpShift", Title = "شیفت کاری" };
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
                    lookup = new LookUp { Code = "LkpClaimUserOnIndicator", Title = "ادعاهای روی شاخص‌ها" };
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
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "ReadForesight", Display = "مشاهده‌گر پیش‌بینی", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "ReadTarget", Display = "مشاهده‌گر هدف", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "ReadPerformance", Display = "مشاهده‌گر عملکرد", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "EditForesight", Display = "ویرایشگر پیش‌بینی", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "EditTarget", Display = "ویرایشگر هدف", OrderNum = 6 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "EditPerformance", Display = "ویرایشگر عملکرد", OrderNum = 7 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "AddForesight", Display = "نگارنده پیش‌بینی", OrderNum = 8 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "AddTarget", Display = "نگارنده هدف", OrderNum = 9 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "AddPerformance", Display = "نگارنده عملکرد", OrderNum = 10 });
                    context.LookUpValues.AddRange(lookUpValue);
                    //------------------------------------
                    context.SaveChanges();
                    lookUpDestination.Clear();
                    lookUpValue.Clear();
                    #endregion

                    #region ClaimUserOnSystem
                    //add lookup
                    //------------------------------------
                    lookup = new LookUp { Code = "LkpClaimUserOnSystem", Title = "ادعاهای روی سیستم" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "ClaimUserOnSystem", ColumnName = "FkLkpClaimUserOnSystemId" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "ChangePasswordForm", Display = "فرم تغییر گذرواژه", OrderNum = 1 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "UserForm", Display = "فرم مدیریت کاربران", OrderNum = 2 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "IndicatorForm", Display = "فرم مدیریت شاخص‌ها", OrderNum = 3 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "IndicatorCategoryForm", Display = "فرم دسته‌بندی‌های شاخص‌ها", OrderNum = 4 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "ClaimUserOnIndicatorForm", Display = "فرم ادعاهای کاربران روی شاخص‌ها", OrderNum = 5 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "ClaimUserOnSystemForm", Display = "فرم ادعاهای کاربران روی سیستم", OrderNum = 6 });
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "IndicatorValueForm", Display = "فرم ورود مقادیر", OrderNum = 7 });
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
                    lookup = new LookUp { Code = "LkpType", Title = "نوع‌های دسته‌بندی" };
                    context.LookUps.Add(lookup);
                    context.SaveChanges();
                    //------------------------------------

                    //add destinations
                    //------------------------------------
                    lookUpDestination.Add(new LookUpDestination { FkLookUpId = lookup.Id, TableName = "Category", ColumnName = "FkLkpCategoryTypeID" });
                    context.LookUpDestinations.AddRange(lookUpDestination);
                    //------------------------------------

                    //add values
                    //------------------------------------
                    lookUpValue.Add(new LookUpValue { FkLookUpId = lookup.Id, Value = "IndicatorCategory", Display = "دسته‌بندی شاخص‌ها", OrderNum = 1 });
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
                    user.Add(new Models.User { UserName = "Admin", PasswordHash = Hasher.HasherHMACSHA512.Hash("Admin + 123"), FullName = "مدیر سیستم", FkLkpWorkCalendarId = context.LookUpValues.Where(x => x.Value == "Nothing" && x.FkLookUp.Code == "LkpWorkCalendar").First().Id });
                    context.Users.AddRange(user);
                    context.SaveChanges();
                    //------------------------------------
                    #endregion
                }
            }

            private void SetBaseClaims(PmisContext context)
            {
                context.Database.EnsureCreated();
                if (!context.ClaimUserOnSystems.Any())
                {
                    List<Models.ClaimUserOnSystem> claim = new List<Models.ClaimUserOnSystem>();
                    #region PrimaryClaims
                    //add claim
                    //------------------------------------
                    claim.Add(new Models.ClaimUserOnSystem { FkLkpClaimUserOnSystemId = context.LookUpValues.Where(x => x.Value == "ChangePasswordForm" && x.FkLookUp.Code == "LkpClaimUserOnSystem").First().Id, FkUserId = context.Users.Where(x => x.UserName == "Admin").First().Id });
                    claim.Add(new Models.ClaimUserOnSystem { FkLkpClaimUserOnSystemId = context.LookUpValues.Where(x => x.Value == "UserForm" && x.FkLookUp.Code == "LkpClaimUserOnSystem").First().Id, FkUserId = context.Users.Where(x => x.UserName == "Admin").First().Id });
                    claim.Add(new Models.ClaimUserOnSystem { FkLkpClaimUserOnSystemId = context.LookUpValues.Where(x => x.Value == "IndicatorForm" && x.FkLookUp.Code == "LkpClaimUserOnSystem").First().Id, FkUserId = context.Users.Where(x => x.UserName == "Admin").First().Id });
                    claim.Add(new Models.ClaimUserOnSystem { FkLkpClaimUserOnSystemId = context.LookUpValues.Where(x => x.Value == "IndicatorCategoryForm" && x.FkLookUp.Code == "LkpClaimUserOnSystem").First().Id, FkUserId = context.Users.Where(x => x.UserName == "Admin").First().Id });
                    claim.Add(new Models.ClaimUserOnSystem { FkLkpClaimUserOnSystemId = context.LookUpValues.Where(x => x.Value == "ClaimUserOnIndicatorForm" && x.FkLookUp.Code == "LkpClaimUserOnSystem").First().Id, FkUserId = context.Users.Where(x => x.UserName == "Admin").First().Id });
                    claim.Add(new Models.ClaimUserOnSystem { FkLkpClaimUserOnSystemId = context.LookUpValues.Where(x => x.Value == "ClaimUserOnSystemForm" && x.FkLookUp.Code == "LkpClaimUserOnSystem").First().Id, FkUserId = context.Users.Where(x => x.UserName == "Admin").First().Id });
                    claim.Add(new Models.ClaimUserOnSystem { FkLkpClaimUserOnSystemId = context.LookUpValues.Where(x => x.Value == "IndicatorValueForm" && x.FkLookUp.Code == "LkpClaimUserOnSystem").First().Id, FkUserId = context.Users.Where(x => x.UserName == "Admin").First().Id });
                    context.ClaimUserOnSystems.AddRange(claim);
                    context.SaveChanges();
                    //------------------------------------
                    #endregion
                }
            }

            private void setBaseCategories(PmisContext context)
            {
                context.Database.EnsureCreated();
                if (!context.Categories.Any())
                {
                    Models.Category categoryParant = new Models.Category();
                    List<Models.Category> category = new List<Models.Category>();
                    #region Category
                    //add category
                    //------------------------------------
                    category.Clear();
                    categoryParant = new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParent = null, Code = "SYS", Title = "نظام‌های مدیریتی", OrderNum = 1 };
                    context.Categories.Add(categoryParant);
                    context.SaveChanges();
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "RiskManagement", Title = "مدیریت ریسک", OrderNum = 1 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "ProcessManagement", Title = "مدیریت فرآیندی", OrderNum = 2 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "TaskLevelStrategicManagement", Title = "مدیریت استراتژیک سطح وظیفه‌ای", OrderNum = 3 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "MacroLevelStrategicManagement", Title = "مدیریت استراتژیک سطح کلان", OrderNum = 4 });
                    context.Categories.AddRange(category);
                    context.SaveChanges();

                    category.Clear();
                    categoryParant = new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParent = null, Code = "STD", Title = "استانداردهای مدیریتی", OrderNum = 2 };
                    context.Categories.Add(categoryParant);
                    context.SaveChanges();
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "Quality", Title = "کیفیت ISO9001", OrderNum = 1 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "Environmental", Title = "زیست ‌محیطی ISO14001", OrderNum = 2 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "SafetyAndHealth", Title = "ایمنی و بهداشت ISO45001", OrderNum = 3 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "Energy", Title = "انرژی ISO50001", OrderNum = 4 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "Laboratory", Title = "آزمایشگاه ISO17025", OrderNum = 5 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "Education", Title = "آموزش ISO10015", OrderNum = 6 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "CustomerComplaints", Title = "شکایت مشتریان ISO10002", OrderNum = 7 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "CustomerSatisfaction", Title = "رضایت مشتریان ISO10004", OrderNum = 8 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "Action", Title = "اقدام", OrderNum = 9 });
                    category.Add(new Models.Category { FklkpTypeId = context.LookUpValues.Where(x => x.Value == "IndicatorCategory" && x.FkLookUp.Code == "LkpType").First().Id, FkParentId = categoryParant.Id, Code = "Other", Title = "سایر", OrderNum = 10 });
                    context.Categories.AddRange(category);
                    context.SaveChanges();
                    //------------------------------------
                    #endregion
                }
            }
        }
    }
}