using System;

namespace MOF.Etimad.Monafasat.SharedKernal
{
    public class ConvertNumberToText
    {
        #region Arabic Number To Word
        /// Group Levels: 987,654,321.234
        /// 234 : Group Level -1
        /// 321 : Group Level 0
        /// 654 : Group Level 1
        /// 987 : Group Level 2
        #endregion

        #region Varaibles & Properties

        /// <summary>
        /// integer part
        /// </summary>
        private decimal _intergerValue;

        /// <summary>
        /// Decimal Part
        /// </summary>
        private Int32 _decimalValue;

        /// <summary>
        /// Number to be converted
        /// </summary>
        public Decimal Number { get; set; }

        /// <summary>
        /// Currency to use
        /// </summary>
        public CurrencyInfo Currency { get; set; }

        /// <summary>
        /// English text to be placed before the generated text
        /// </summary>
        public String EnglishPrefixText { get; set; }

        /// <summary>
        /// English text to be placed after the generated text
        /// </summary>
        public String EnglishSuffixText { get; set; }

        /// <summary>
        /// Arabic text to be placed before the generated text
        /// </summary>
        public String ArabicPrefixText { get; set; }

        /// <summary>
        /// Arabic text to be placed after the generated text
        /// </summary>
        public String ArabicSuffixText { get; set; }
        #endregion

        #region General

        /// <summary>
        /// Constructor: short version
        /// </summary>
        /// <param name="number">Number to be converted</param>
        /// <param name="currency">Currency to use</param>
        public ConvertNumberToText(Decimal number, CurrencyInfo currency)
        {
            InitializeClass(number, currency, String.Empty, "only.", "لا غير.");
        }

        /// <summary>
        /// Constructor: Full Version
        /// </summary>
        /// <param name="number">Number to be converted</param>
        /// <param name="currency">Currency to use</param>
        /// <param name="englishPrefixText">English text to be placed before the generated text</param>
        /// <param name="englishSuffixText">English text to be placed after the generated text</param>
        /// <param name="arabicPrefixText">Arabic text to be placed before the generated text</param>
        /// <param name="arabicSuffixText">Arabic text to be placed after the generated text</param>
        public ConvertNumberToText(Decimal number, CurrencyInfo currency, String englishPrefixText, String englishSuffixText, String arabicPrefixText, String arabicSuffixText)
        {
            InitializeClass(number, currency, englishPrefixText, englishSuffixText, arabicSuffixText);
        }

        /// <summary>
        /// Initialize Class Varaibles
        /// </summary>
        /// <param name="number">Number to be converted</param>
        /// <param name="currency">Currency to use</param>
        /// <param name="englishPrefixText">English text to be placed before the generated text</param>
        /// <param name="englishSuffixText">English text to be placed after the generated text</param>
        /// <param name="arabicPrefixText">Arabic text to be placed before the generated text</param>
        /// <param name="arabicSuffixText">Arabic text to be placed after the generated text</param>
        private void InitializeClass(Decimal number, CurrencyInfo currency, String englishPrefixText, String englishSuffixText, String arabicSuffixText)
        {
            Number = number;
            Currency = currency;
            EnglishPrefixText = englishPrefixText;
            EnglishSuffixText = englishSuffixText;
            ArabicSuffixText = arabicSuffixText;

            ExtractIntegerAndDecimalParts();
        }

        /// <summary>
        /// Get Proper Decimal Value
        /// </summary>
        /// <param name="decimalPart">Decimal Part as a String</param>
        /// <returns></returns>
        private string GetDecimalValue(string decimalPart)
        {
            string result = String.Empty;

            if (Currency.PartPrecision != decimalPart.Length)
            {
                result = String.Format("{0}.{1}", decimalPart.Substring(0, Currency.PartPrecision), decimalPart.Substring(Currency.PartPrecision, decimalPart.Length - Currency.PartPrecision));

                result = (Math.Round(Convert.ToDecimal(result))).ToString();
            }
            else
                result = decimalPart;

            for (int i = 0; i < Currency.PartPrecision - result.Length; i++)
            {
                result += "0";
            }

            return result;
        }

        /// <summary>
        /// Eextract Interger and Decimal parts
        /// </summary>
        private void ExtractIntegerAndDecimalParts()
        {
            String[] splits = Number.ToString().Split('.');

            _intergerValue = decimal.Parse(splits[0]);

            if (splits.Length > 1 && splits[1] != "0")
                _decimalValue = Convert.ToInt32(GetDecimalValue(splits[1]));
        }
        #region Varaibles

        private static string[] arabicOnes =
           new string[] {
            String.Empty, "واحد", "اثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة",
            "عشرة", "أحد عشر", "اثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر"
        };

        private static string[] arabicFeminineOnes =
           new string[] {
            String.Empty, "إحدى", "اثنتان", "ثلاث", "أربع", "خمس", "ست", "سبع", "ثمان", "تسع",
            "عشر", "إحدى عشرة", "اثنتا عشرة", "ثلاث عشرة", "أربع عشرة", "خمس عشرة", "ست عشرة", "سبع عشرة", "ثماني عشرة", "تسع عشرة"
        };

        private static string[] arabicTens =
            new string[] {
            "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون"
        };

        private static string[] arabicHundreds =
            new string[] {
            "", "مائة", "مئتان", "ثلاثمائة", "أربعمائة", "خمسمائة", "ستمائة", "سبعمائة", "ثمانمائة","تسعمائة"
        };

        private static string[] arabicAppendedTwos =
            new string[] {
            "مئتا", "ألفا", "مليونا", "مليارا", "تريليونا", "كوادريليونا", "كوينتليونا", "سكستيليونا"
        };

        private static string[] arabicTwos =
            new string[] {
            "مئتان", "ألفان", "مليونان", "ملياران", "تريليونان", "كوادريليونان", "كوينتليونان", "سكستيليونان"
        };

        private static string[] arabicGroup =
            new string[] {
            "مائة", "ألف", "مليون", "مليار", "تريليون", "كوادريليون", "كوينتليون", "سكستيليون"
        };

        private static string[] arabicAppendedGroup =
            new string[] {
            "", "ألفاً", "مليوناً", "ملياراً", "تريليوناً", "كوادريليوناً", "كوينتليوناً", "سكستيليوناً"
        };

        private static string[] arabicPluralGroups =
            new string[] {
            "", "آلاف", "ملايين", "مليارات", "تريليونات", "كوادريليونات", "كوينتليونات", "سكستيليونات"
        };
        #endregion

        /// <summary>
        /// Get Feminine Status of one digit
        /// </summary>
        /// <param name="digit">The Digit to check its Feminine status</param>
        /// <param name="groupLevel">Group Level</param>
        /// <returns></returns>
        private string GetDigitFeminineStatus(int digit, int groupLevel)
        {
            if (groupLevel == -1)
            { // if it is in the decimal part
                if (Currency.IsCurrencyPartNameFeminine)
                    return arabicFeminineOnes[digit]; // use feminine field
                else
                    return arabicOnes[digit];
            }
            else
                if (groupLevel == 0)
            {
                if (Currency.IsCurrencyNameFeminine)
                    return arabicFeminineOnes[digit];// use feminine field
                else
                    return arabicOnes[digit];
            }
            else
                return arabicOnes[digit];
        }

        /// <summary>
        /// Process a group of 3 digits
        /// </summary>
        /// <param name="groupNumber">The group number to process</param>
        /// <returns></returns>
        private string ProcessArabicGroup(int groupNumber, int groupLevel, Decimal remainingNumber)
        {
            Int32 tens = groupNumber % 100;

            int hundreds = groupNumber / 100;

            string retVal = String.Empty;

            if (hundreds > 0)
            {
                if (tens == 0 && hundreds == 2) // حالة المضاف
                    retVal = String.Format("{0}", arabicAppendedTwos[0]);
                else //  الحالة العادية
                    retVal = String.Format("{0}", arabicHundreds[hundreds]);
            }

            if (tens > 0)
            {
                if (tens < 20)
                { // if we are processing under 20 numbers
                    if (tens == 2 && hundreds == 0 && groupLevel > 0)
                    { // This is special case for number 2 when it comes alone in the group
                        if (_intergerValue == 2000 || _intergerValue == 2000000 || _intergerValue == 2000000000 || _intergerValue == 2000000000000 || _intergerValue == 2000000000000000 || _intergerValue == 2000000000000000000)
                            retVal = String.Format("{0}", arabicAppendedTwos[groupLevel]); // في حالة الاضافة
                        else
                            retVal = String.Format("{0}", arabicTwos[groupLevel]);//  في حالة الافراد
                    }
                    else
                    { // General case
                        if (retVal != String.Empty)
                            retVal += " و ";

                        if (tens == 1 && groupLevel > 0)
                            retVal += arabicGroup[groupLevel];
                        else
                            if ((tens == 1 || tens == 2) && (groupLevel == 0 || groupLevel == -1) && hundreds == 0 && remainingNumber == 0)
                            retVal += String.Empty; // Special case for 1 and 2 numbers like: ليرة سورية و ليرتان سوريتان
                        else
                            retVal += GetDigitFeminineStatus(tens, groupLevel);// Get Feminine status for this digit
                    }
                }
                else
                {
                    int ones = tens % 10;
                    tens = (tens / 10) - 2; // 20's offset

                    if (ones > 0)
                    {
                        if (retVal != String.Empty)
                            retVal += " و ";

                        // Get Feminine status for this digit
                        retVal += GetDigitFeminineStatus(ones, groupLevel);
                    }

                    if (retVal != String.Empty)
                        retVal += " و ";

                    // Get Tens text
                    retVal += arabicTens[tens];
                }
            }

            return retVal;
        }

        /// <summary>
        /// Convert stored number to words using selected currency
        /// </summary>
        /// <returns></returns>
        public string ConvertToArabic()
        {
            Decimal tempNumber = Number;
            if (Number == 0)
                return "صفر";

            // Get Text for the decimal part
            string decimalString = ProcessArabicGroup(_decimalValue, -1, 0);

            string retVal = String.Empty;
            Byte group = 0;
            while (Number >= 1)
            {
                // seperate number into groups
                int numberToProcess = (int)(Number % 1000);

                Number = Number / 1000;

                // convert group into its text
                string groupDescription = ProcessArabicGroup(numberToProcess, group, Math.Floor(Number));

                if (groupDescription != String.Empty)
                { // here we add the new converted group to the previous concatenated text
                    if (group > 0)
                    {
                        if (retVal != String.Empty)
                            retVal = String.Format("{0} {1}", "و", retVal);

                        if (numberToProcess != 2)
                        {
                            if (numberToProcess % 100 != 1)
                            {
                                if (numberToProcess >= 3 && numberToProcess <= 10) // for numbers between 3 and 9 we use plural name
                                    retVal = String.Format("{0} {1}", arabicPluralGroups[group], retVal);
                                else
                                {
                                    if (retVal != String.Empty) // use appending case
                                        retVal = String.Format("{0} {1}", arabicAppendedGroup[group], retVal);
                                    else
                                        retVal = String.Format("{0} {1}", arabicGroup[group], retVal); // use normal case
                                }
                            }
                        }
                    }

                    retVal = String.Format("{0} {1}", groupDescription, retVal);
                }

                group++;
            }

            String formattedNumber = String.Empty;
            formattedNumber += (ArabicPrefixText != String.Empty) ? String.Format("{0} ", ArabicPrefixText) : String.Empty;
            formattedNumber += (retVal != String.Empty) ? retVal : String.Empty;
            if (_intergerValue != 0)
            { // here we add currency name depending on _intergerValue : 1 ,2 , 3--->10 , 11--->99
                int remaining100 = (int)(_intergerValue % 100);

                if (remaining100 == 0)
                    formattedNumber += Currency.Arabic1CurrencyName;
                else
                    if (remaining100 == 1)
                    formattedNumber += Currency.Arabic1CurrencyName;
                else
                        if (remaining100 == 2)
                {
                    if (_intergerValue == 2)
                        formattedNumber += Currency.Arabic2CurrencyName;
                    else
                        formattedNumber += Currency.Arabic1CurrencyName;
                }
                else
                            if (remaining100 >= 3 && remaining100 <= 10)
                    formattedNumber += Currency.Arabic310CurrencyName;
                else
                                if (remaining100 >= 11 && remaining100 <= 99)
                    formattedNumber += Currency.Arabic1199CurrencyName;
            }
            formattedNumber += (_decimalValue != 0) ? " و " : String.Empty;
            formattedNumber += (_decimalValue != 0) ? decimalString : String.Empty;
            if (_decimalValue != 0)
            { // here we add currency part name depending on _intergerValue : 1 ,2 , 3--->10 , 11--->99
                formattedNumber += " ";

                int remaining100 = (int)(_decimalValue % 100);

                if (remaining100 == 0)
                    formattedNumber += Currency.Arabic1CurrencyPartName;
                else
                    if (remaining100 == 1)
                    formattedNumber += Currency.Arabic1CurrencyPartName;
                else
                        if (remaining100 == 2)
                    formattedNumber += Currency.Arabic2CurrencyPartName;
                else
                            if (remaining100 >= 3 && remaining100 <= 10)
                    formattedNumber += Currency.Arabic310CurrencyPartName;
                else
                                if (remaining100 >= 11 && remaining100 <= 99)
                    formattedNumber += Currency.Arabic1199CurrencyPartName;
            }
            formattedNumber += (ArabicSuffixText != String.Empty) ? String.Format(" {0}", ArabicSuffixText) : String.Empty;

            return formattedNumber;
        }
        public string ConvertToArabicWithoutSuffix()
        {
            Decimal tempNumber = Number;
            if (Number == 0)
                return "صفر";

            // Get Text for the decimal part
            string decimalString = ProcessArabicGroup(_decimalValue, -1, 0);

            string retVal = String.Empty;
            Byte group = 0;
            while (Number >= 1)
            {
                // seperate number into groups
                int numberToProcess = (int)(Number % 1000);

                Number = Number / 1000;

                // convert group into its text
                string groupDescription = ProcessArabicGroup(numberToProcess, group, Math.Floor(Number));

                if (groupDescription != String.Empty)
                { // here we add the new converted group to the previous concatenated text
                    if (group > 0)
                    {
                        if (retVal != String.Empty)
                            retVal = String.Format("{0} {1}", "و", retVal);

                        if (numberToProcess != 2)
                        {
                            if (numberToProcess % 100 != 1)
                            {
                                if (numberToProcess >= 3 && numberToProcess <= 10) // for numbers between 3 and 9 we use plural name
                                    retVal = String.Format("{0} {1}", arabicPluralGroups[group], retVal);
                                else
                                {
                                    if (retVal != String.Empty) // use appending case
                                        retVal = String.Format("{0} {1}", arabicAppendedGroup[group], retVal);
                                    else
                                        retVal = String.Format("{0} {1}", arabicGroup[group], retVal); // use normal case
                                }
                            }
                        }
                    }

                    retVal = String.Format("{0} {1}", groupDescription, retVal);
                }

                group++;
            }

            String formattedNumber = String.Empty;
            formattedNumber += (retVal != String.Empty) ? retVal : String.Empty;
            formattedNumber += (_decimalValue != 0) ? decimalString : String.Empty;
            formattedNumber += (ArabicSuffixText != String.Empty) ? String.Format(" {0}", ArabicSuffixText) : String.Empty;

            return formattedNumber;
        }
        #endregion
    }


    public class CurrencyInfo
    {
        /// <summary>
        /// Currency ID
        /// </summary>
        public int CurrencyID { get; set; }

        /// <summary>
        /// Standard Code
        /// Saudi Riyal : SAR
        /// UAE Dirham: AED
        /// </summary>
        public string CurrencyCode { get; set; }


        /// </summary>
        public Boolean IsCurrencyNameFeminine { get; set; }

        /// <summary>
        /// English Currency Name for single use
        /// Saudi Riyal : SAR

        /// </summary>
        public string EnglishCurrencyName { get; set; }

        /// <summary>
        /// English Plural Currency Name for Numbers over 1
        /// Saudi Riyal : SAR
        /// UAE Dirhams
        /// </summary>
        public string EnglishPluralCurrencyName { get; set; }

        /// <summary>
        /// Arabic Currency Name for 1 unit only
        /// ريال سعودي
        /// درهم إماراتي
        /// </summary>
        public string Arabic1CurrencyName { get; set; }

        /// <summary>
        /// Arabic Currency Name for 2 units only
        /// ريالان سعوديان
        /// درهمان إماراتيان
        /// </summary>
        public string Arabic2CurrencyName { get; set; }

        /// <summary>
        /// Arabic Currency Name for 3 to 10 units
        /// خمس ريالات سعودية
        /// خمسة دراهم إماراتية
        /// </summary>
        public string Arabic310CurrencyName { get; set; }

        /// <summary>
        /// Arabic Currency Name for 11 to 99 units
        /// خمس و سبعون ريالاٌ سعودياٌ
        /// خمسة و سبعون درهماً إماراتياً
        /// </summary>
        public string Arabic1199CurrencyName { get; set; }

        /// <summary>
        /// Decimal Part Precision
        /// for Saudi Riyal : SAR: 2 ( 1 SAR = 100 parts)
        /// for Tunisian Dinars: 3 ( 1 TND = 1000 parts)
        /// </summary>
        public Byte PartPrecision { get; set; }

        /// <summary>
        /// Is the currency part name feminine ( Mua'anath مؤنث)
        /// هللة : مؤنث = true
        /// قرش : مذكر = false
        /// </summary>
        public Boolean IsCurrencyPartNameFeminine { get; set; }

        /// <summary>
        /// English Currency Part Name for single use
        /// Piaster
        /// Fils
        /// </summary>
        public string EnglishCurrencyPartName { get; set; }

        /// <summary>
        /// English Currency Part Name for Plural
        /// Piasters
        /// Fils
        /// </summary>
        public string EnglishPluralCurrencyPartName { get; set; }

        /// <summary>
        /// Arabic Currency Part Name for 1 unit only
        /// قرش
        /// هللة
        /// </summary>
        public string Arabic1CurrencyPartName { get; set; }

        /// <summary>
        /// Arabic Currency Part Name for 2 unit only
        /// قرشان
        /// هللتان
        /// </summary>
        public string Arabic2CurrencyPartName { get; set; }

        /// <summary>
        /// Arabic Currency Part Name for 3 to 10 units
        /// قروش
        /// هللات
        /// </summary>
        public string Arabic310CurrencyPartName { get; set; }

        /// <summary>
        /// Arabic Currency Part Name for 11 to 99 units
        /// قرشاً
        /// هللةً
        /// </summary>
        public string Arabic1199CurrencyPartName { get; set; }
        public enum Currencies { Syria = 0, UAE, SaudiArabia, Tunisia, Gold, Others };

        public CurrencyInfo(Currencies currency)
        {

            switch (currency)
            {
                case Currencies.Syria:
                    break;
                case Currencies.UAE:
                    break;
                case Currencies.SaudiArabia:
                    CurrencyID = 2;
                    CurrencyCode = "SAR";
                    IsCurrencyNameFeminine = false;
                    EnglishCurrencyName = "Saudi Riyal";
                    EnglishPluralCurrencyName = "Saudi Riyals";
                    EnglishCurrencyPartName = "Halala";
                    EnglishPluralCurrencyPartName = "Halalas";
                    Arabic1CurrencyName = "ريال سعودي";
                    Arabic2CurrencyName = "ريالان سعوديان";
                    Arabic310CurrencyName = "ريالات سعودية";
                    Arabic1199CurrencyName = "ريالاً سعودياً";
                    Arabic1CurrencyPartName = "هللة";
                    Arabic2CurrencyPartName = "هللتان";
                    Arabic310CurrencyPartName = "هللات";
                    Arabic1199CurrencyPartName = "هللة";
                    PartPrecision = 2;
                    IsCurrencyPartNameFeminine = true;
                    break;
                case Currencies.Tunisia:
                    break;
                case Currencies.Gold:
                    break;
                case Currencies.Others:
                    CurrencyID = 5;
                    CurrencyCode = "";
                    IsCurrencyNameFeminine = false;
                    EnglishCurrencyName = "";
                    EnglishPluralCurrencyName = "";
                    EnglishCurrencyPartName = "";
                    EnglishPluralCurrencyPartName = "";
                    Arabic1CurrencyName = "";
                    Arabic2CurrencyName = "";
                    Arabic310CurrencyName = "";
                    Arabic1199CurrencyName = "";
                    Arabic1CurrencyPartName = "";
                    Arabic2CurrencyPartName = "";
                    Arabic310CurrencyPartName = "";
                    Arabic1199CurrencyPartName = "";
                    PartPrecision = 2;
                    IsCurrencyPartNameFeminine = true;
                    break;
                default:
                    CurrencyID = 2;
                    CurrencyCode = "SAR";
                    IsCurrencyNameFeminine = false;
                    EnglishCurrencyName = "Saudi Riyal";
                    EnglishPluralCurrencyName = "Saudi Riyals";
                    EnglishCurrencyPartName = "Halala";
                    EnglishPluralCurrencyPartName = "Halalas";
                    Arabic1CurrencyName = "ريال سعودي";
                    Arabic2CurrencyName = "ريالان سعوديان";
                    Arabic310CurrencyName = "ريالات سعودية";
                    Arabic1199CurrencyName = "ريالاً سعودياً";
                    Arabic1CurrencyPartName = "هللة";
                    Arabic2CurrencyPartName = "هللتان";
                    Arabic310CurrencyPartName = "هللات";
                    Arabic1199CurrencyPartName = "هللة";
                    PartPrecision = 2;
                    IsCurrencyPartNameFeminine = true;
                    break;
            }





            //case Currencies.Gold:
            //    CurrencyID = 4;
            //    CurrencyCode = "XAU";
            //    IsCurrencyNameFeminine = false;
            //    EnglishCurrencyName = "Gram";
            //    EnglishPluralCurrencyName = "Grams";
            //    EnglishCurrencyPartName = "Milligram";
            //    EnglishPluralCurrencyPartName = "Milligrams";
            //    Arabic1CurrencyName = "جرام";
            //    Arabic2CurrencyName = "جرامان";
            //    Arabic310CurrencyName = "جرامات";
            //    Arabic1199CurrencyName = "جراماً";
            //    Arabic1CurrencyPartName = "ملجرام";
            //    Arabic2CurrencyPartName = "ملجرامان";
            //    Arabic310CurrencyPartName = "ملجرامات";
            //    Arabic1199CurrencyPartName = "ملجراماً";
            //    PartPrecision = 2;
            //    IsCurrencyPartNameFeminine = false;
            //    break;


        }


    }
}