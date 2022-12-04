using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
namespace Utilerias
{
    public class Token
    {
        public string Usuario { get; set; }
        public string NumeroEmpleado { get; set; }
        public string Idioma { get; set; }
        public string IdiomaSAP { get; set; }
        public string Grupo { get; set; }

        public Token()
        {
            Usuario = string.Empty;
            NumeroEmpleado = string.Empty;
            Idioma = string.Empty;
            IdiomaSAP = string.Empty;
            Grupo = string.Empty;
        }

        private static string Separador = "*|*~*¬";

        private static string KEY = "B54CAHIJKDEF6G6LM9RSTU5NPQ25VWZabXYcd8h28ij5efgk2n1olmpq6turs4vw5yzx7";

        private static string EncryptKey(string cadena)
        {
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;

            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(cadena);

            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Token.KEY));
            hashmd5.Clear();

            //Algoritmo 3DES
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
            tdes.Clear();

            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }

        public static string Decrypt(string clave)
        {
            string cadena = string.Empty;
            try
            {
                byte[] keyArray;
                //convierte el texto en una secuencia de bytes
                byte[] Array_a_Descifrar = Convert.FromBase64String(clave);

                //se llama a las clases que tienen los algoritmos
                //de encriptación se le aplica hashing
                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Token.KEY));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
                tdes.Clear();
                cadena = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cadena;
        }

        public static string GenerarToken(string usuario, string numEmpleado, string idioma, string idiomaSAP, string grupo)
        {
            return EncryptKey(numEmpleado + Separador + DateTime.Now.ToString("HHmmssffff") + Separador +
                              usuario + Separador + DateTime.Now.ToString("ssHHffffmm") + Separador +
                              idioma + Separador + DateTime.Now.ToString("ssffffHHmm") + Separador +
                              idiomaSAP + Separador + DateTime.Now.ToString("ffffssmmHH") + Separador +
                              grupo + Separador + DateTime.Now.ToString("ssHHmmffff"));
        }

        public static string GenerarPSW()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!%$#@&*",
                    contraseniaAleatoria = string.Empty;
            int longitud = caracteres.Length,
                longitudContrasenia = 10;
            char letra;

            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }

            return contraseniaAleatoria;
        }

        public static string EncrytPSW(string PSW)
        {
            return EncryptKey(Separador + PSW.Substring(0, PSW.Length / 2) + Separador + PSW.Substring((PSW.Length / 2)) + Separador);
        }

        public static Token DecryptKey(string clave)
        {
            Token respuesta = new Token();
            try
            {
                string[] cadena = Token.Decrypt(clave).Split(new string[] { Token.Separador }, StringSplitOptions.None);
                respuesta.NumeroEmpleado = cadena[0];
                respuesta.Usuario = cadena[2];
                respuesta.Idioma = cadena[4];
                respuesta.IdiomaSAP = cadena[6];
                respuesta.Grupo = cadena[8];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //se regresa en forma de cadena
            return respuesta;
        }
    }
}


