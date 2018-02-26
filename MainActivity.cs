using Android.App;
using Android.Widget;
using Android.OS;

namespace PhoneApp
{
    [Activity(Label = "Phone App", MainLauncher = true, Icon = "@drawable/icon")]   //Esto escribe en el manifiesto
    //Label -> texto superior que aparecerá en la pantalla de la app.
    //Icon -> imágen que se mostrará junto al texto
    //MainLauncher -> Actividad inicial de la app.
    public class MainActivity : Activity
    {
        static readonly System.Collections.Generic.List<string> PhoneNumbers = new System.Collections.Generic.List<string>(); // Crea una lista vacía que pueda ser llenada con los números telefónicos.

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);  //carga el oncreate por default primero
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            Validate();
            //Se obtiene una referencia a los controles que fueron creados en el archivo del diseño de la interfaz de usuario Main.axml
            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);  //se almacenan los recursos del layout en variables utilizarlas luego
            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);
            var CallHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);


            CallButton.Enabled = false; //deshabilita el botón llamar, ya que en un inicio no tendría ningún número
            var TranslatedNumber = string.Empty;    //variable que almacenará el número telefónico convertido a solo números

            //el código será ejecutado al presionar el botón Convertir
            TranslateButton.Click += (object sender, System.EventArgs e) => //=> significa lambda, código predefinido para un evento. El +- es para sobreescribir el Click
            {
                var Translator = new PhoneTranslator(); //crea un nuevo objeto de la clase PhoneTranslator, que se encarga de convertir las letras y guiones en números
                TranslatedNumber = Translator.ToNumber(PhoneNumberText.Text);   //envía a la clase el número alfanumérico para la conversión y se almacena en la variable TranslatedNumber

                if (string.IsNullOrWhiteSpace(TranslatedNumber))    // IsNullOrWhiteSpace es una función que se encarga de ver si un string está vació o tiene espacios
                //Si está vació o tiene espacios 
                {
                    // No hay número a llamar  
                    CallButton.Text = "Llamar";
                    CallButton.Enabled = false; //desactiva el botón
                }
                else
                {
                    // Hay un posible número telefónico a llamar
                    CallButton.Text = $"Llamar al {TranslatedNumber}";  //concatenación de variables string
                    CallButton.Enabled = true;
                }
            };

            // Acción para el botón llamar
            CallButton.Click += (object sender, System.EventArgs e) =>
            {
                // Intentar marcar el número telefónico
                var CallDialog = new AlertDialog.Builder(this);     // AlertDilog es el mensaje por default negro de Android. Builder es "generador" para construírlo.
                // Entonces se crea un objeto Generador de AlertDialog, justamente para ejecutar esta alerta.
                CallDialog.SetMessage($"Llamar al número {TranslatedNumber}?"); //El mensaje que emitirá el cartel
                CallDialog.SetNeutralButton("Llamar", delegate      //Acción de llamada si preciona el botón llamar. "Delegate" es la delegación de lo que va a hacer el botón.
                {
                    // Cada vez que se ejecuta el botón agrega el número marcado a la lista (array) de números marcados
                    PhoneNumbers.Add(TranslatedNumber);
                    // Habilitar el botón CallHistotyButton porque ya hay un número
                    CallHistoryButton.Enabled = true;

                    // Crea un intento para marcar el número telefónico
                    var CallIntent =    // Se envía una intención para inciar la aplicación phone de android
                    new Android.Content.Intent(Android.Content.Intent.ActionCall); // Se envía un sólo parámetro ya que es sólo una acción (Lanza la aplicación de llamada del teléfono)
                    CallIntent.SetData( Android.Net.Uri.Parse($"tel:{TranslatedNumber}"));  // se le pasa el número a llamar al mensaje
                    StartActivity(CallIntent); // Ejecuta el intent
                });

                CallDialog.SetNegativeButton("Cancelar", delegate { });
                // Mostrar el cuadro de diálogo al usuario y esperar una respuesta.
                CallDialog.Show();  //finalmente se muestra el objeto, en este caso la alerta.
            };

            // Función para la acción del botón de historial de llamadas
            CallHistoryButton.Click += (sender, e) =>
            {
                var Intent = new Android.Content.Intent(this, typeof(CallHistoryActivity)); // Se agrega un intent que se encuentra en la clase "content" de la clase "android"
                // "this" se refiere al contexto actual (contexto donde se está ejecutando el código, este activity)
                // "typof" es el "tipo de bloque" que nos interesa conectar, en este caso es la pantalla (Activity) CallHistoryActivity
                // Es decir, "this" (este contexto, esta activty) es el receptor y CallHistoryActivity el emisor

                Intent.PutStringArrayListExtra("phone_numbers", PhoneNumbers); //se agrega el array PhoneNumbers al mensaje (intent), el string inicial es un alias
                StartActivity(Intent);
            };

        }


    }
}

