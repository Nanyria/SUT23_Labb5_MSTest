using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    public class AsciiHeadliner
    {
        public static void PrintHeadliner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string Headliner = @"  ____        _   _   _      _   ____                                        
 |  _ \  ___ | |_| \ | | ___| |_|  _ \ _   _ _ __   __ _ _ __ ___   ___  ___ 
 | | | |/ _ \| __|  \| |/ _ \ __| | | | | | | '_ \ / _` | '_ ` _ \ / _ \/ __|
 | |_| | (_) | |_| |\  |  __/ |_| |_| | |_| | | | | (_| | | | | | | (_) \__ \
 |____/ \___/ \__|_| \_|\___|\__|____/ \__, |_| |_|\__,_|_| |_| |_|\___/|___/
                                       |___/                                 ";
            Console.WriteLine(Headliner);
            Console.ResetColor();
        }
    }
}