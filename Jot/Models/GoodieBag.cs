/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/

using System;
using System.Collections.Generic;

namespace Jarloo.Jot.Models
{
    [Serializable]
    public class GoodieBag
    {
        public List<IWidget> Widgets { get; set; }
    }
}