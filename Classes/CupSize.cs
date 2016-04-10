using System;
using System.ComponentModel;

namespace CoffeePods.Classes
{
    [Flags]
    public enum CupSizeEnum : short
    {
        [Description("Ristretto 25ml")]
        Ristretto = 1,

        [Description("Espresso 40ml")]
        Espresso = 2,

        [Description("Lungo 110ml")]
        Lungo = 4
    }
}
