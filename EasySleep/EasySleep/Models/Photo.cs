using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EasySleep.Model
{
    public class Photo
    {

        public int Id { get; set; }
        public String Img { get; set; }

        public Photo (int id, string img)
        {
            Id = id;
            Img = img;
        }
    }
}
