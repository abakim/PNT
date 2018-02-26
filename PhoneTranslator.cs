using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    public class PhoneTranslator
    {
        string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string Numbers = "22233344455566677778889999";

        public string ToNumber(string alfanumericPhoneNumber)   //recibe el número con letras
        {
            var NumericPhoneNumber = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(alfanumericPhoneNumber)) //si el dato no es nulo o tiene espacios en blanco
            {
                alfanumericPhoneNumber = alfanumericPhoneNumber.ToUpper();  //pasa todo a mayúsculas
                foreach (var c in alfanumericPhoneNumber)   //recorre el string
                {
                    if ("0123456789".Contains(c))   //si un número está contenido en c
                    {
                        NumericPhoneNumber.Append(c);   //lo agrega al nuevo vector (o string)
                    }
                    else
                    {
                        var Index = Letters.IndexOf(c); //obtiene el índice del abecedario
                        if (Index >= 0) //si el índice es mayor que 0
                        {
                            NumericPhoneNumber.Append(Numbers[Index]);  //selecciona el número correspondiente a la letra utilizando el índice
                        }
                    }
                }
            }
            return NumericPhoneNumber.ToString();   //pasa el número finalmente a String y lo retorna
        }
    }
}