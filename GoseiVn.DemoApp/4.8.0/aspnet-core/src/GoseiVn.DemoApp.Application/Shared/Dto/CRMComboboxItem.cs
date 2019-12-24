using System;
using System.Collections.Generic;
using System.Text;

namespace GoseiVn.DemoApp.Shared.Dto
{
   public class CRMComboboxItem
    {
        public long Value { get; set; }

        public string DisplayText { get; set; }
        public string DisplayCode { get; set; }

        public CRMComboboxItem()
        {
        }

        public CRMComboboxItem(long value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}
