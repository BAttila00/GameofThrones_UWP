using GameofThrones.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameofThrones.ViewModels {
    static class HolderClassSingleton {
        public static List<Book> ThroneBooks { get; set; }
        public static List<Character> Characters { get; set; }
        static public List<House> Houses { get; set; }

        static HolderClassSingleton() {
            ThroneBooks = new List<Book>();
            Characters = new List<Character>();
            Houses = new List<House>();
        }
    }
}
