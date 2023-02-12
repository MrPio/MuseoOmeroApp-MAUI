using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseoOmero.DataTemplateSelectors
{
    public class FlyoutDataTemplateSelectorWin : DataTemplateSelector
    {
        //public DataTemplate OnTemplate { get; set; }
        //public DataTemplate OffTemplate { get; set; }

        //protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        //{
        //    throw new NotImplementedException();
        //    return OnTemplate;
        //}

        public DataTemplate OnTemplate { get; set; }
        public DataTemplate OffTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return OnTemplate;
        }
    }
}
