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
    [Activity(Label = "@string/CallHistory")]
    public class CallHistoryActivity : ListActivity // hereda la clase ListActivity en vez de Activity.
    // La clase ListActivity es un tipo de Activity que muestra una lista de elementos mediante un enlace a una fuente de datos tal como un arreglo.
    // En este caso se va a utilizar para hacer una lista de historial de llamadas.
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            // Se crea una ListActivity y se llena mediante código, por lo tanto, no es necesario crear un nuevo archivo de diseño.axml para esta Activity.
            // Es decir, no tendrá un diseño predefinido con botones gráficos y demás si no que se llenará según lo estamos indicando.
            var PhoneNumbers = Intent.Extras.GetStringArrayList("phone_numbers") ?? new string[0]; //???
            this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, PhoneNumbers);
        }
    }
}