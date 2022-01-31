//List of accepted characters for a potential input message
char[] charMap = new char[] { ' ','a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

//Select your favourite pair of prime numbers
int p = 7;
int q = 11;
Console.WriteLine("p: " + p + "\nq: " + q + "\n");

//Compute n and phi
int n = p * q;
int phi = (p - 1) * (q - 1);
Console.WriteLine("n: " + n + "\nphi: " + phi + "\n");

//Select an e and compute the d
int e = selectE(phi);
int d = modInverse(e, phi);
Console.WriteLine("e: " + e + "\nd: " + d + "\n");

//Prompt user for input message to encrypt
Console.Write("Input unencrypted text: ");
string textUnencrypted = Console.ReadLine();

//Convert string to char[] so we may process each character individually
char[] character = textUnencrypted.ToCharArray();

string encryptedEncoding = "";
string rawEncoding = "";

//For each character in the input message,
for (int i = 0; i < textUnencrypted.Length; i++)
{
    int index = 0;
    //find that character in the charMap table to determine it's index (raw mapping)
    for (int j = 0; j < charMap.Length; j++)
    {
        if (character[i] == charMap[j])
        {
            index = j;
            break;
        }
    }
    //Convert each character to its encoded rsa value by using rsaEncrypt function
    encryptedEncoding += Convert.ToString(rsaEncrypt(index, e, n));
    encryptedEncoding += "-";
    //Store the raw encoding for display purposes
    rawEncoding += index.ToString();
    rawEncoding += "-";
}
//Output the raw encoding
rawEncoding = rawEncoding.Substring(0, rawEncoding.Length - 1);
Console.WriteLine("Raw mapping: " + rawEncoding);
//Output the encrypted encoding
encryptedEncoding = encryptedEncoding.Substring(0, encryptedEncoding.Length - 1);
Console.WriteLine("\nEncrypted mapping: " + encryptedEncoding);

//Split the encoded string into its symbols and store those symbols as integers
string[] cipherArrayString = encryptedEncoding.Split('-');
int[] cipherArrayInt = new int[cipherArrayString.Length];
for (int i = 0; i < cipherArrayInt.Length; i++)
{
    cipherArrayInt[i] = Convert.ToInt32(cipherArrayString[i]);
}

//Convert each encrypted symbol in cipherArrayInt to its corresponding decrypted symbol using rsaDecrypt function
string decryptedEncoding = "";
for (int i = 0; i < cipherArrayInt.Length; i++)
{
    decryptedEncoding += rsaDecrypt(cipherArrayInt[i], d, n).ToString();
    decryptedEncoding += "-";
}
//Output the raw encoding
decryptedEncoding = decryptedEncoding.Substring(0, decryptedEncoding.Length - 1);
Console.WriteLine("\nDecrypted mapping: " + decryptedEncoding);

//Build the output string by removing delimiters and doing a lookup against original charMap
string textDecrypted = "";
string[] decryptedSymbol = decryptedEncoding.Split('-');
for (int i = 0; i < decryptedSymbol.Length; i++)
{
    textDecrypted += charMap[Convert.ToInt32(decryptedSymbol[i])];
}

//Display the decrypted message
Console.WriteLine("Decrypted text: " + textDecrypted);

//Choose a number e such that 1 < e < phi(n) and gcd(e,phi(n))=1
int selectE(int phi)
{
    for (int e = 2; e < phi; e++)
    {
        if (gcd(e, phi) == 1) return e;
    }
    return -1;
}
//Greatest common divisor GCD function (sometimes called Highest common factor HCF)
int gcd(int a, int b)
{
    if (a == 0) return b;
    return gcd(b % a, a);
}
//Compute d as the inverse of e modulo phi(n), aka e*d mod phi(n) = 1
int modInverse(int e, int phi)
{
    for (int d = 1; d < phi; d++)
        if ((e * d) % phi == 1) return d;
    return -1;
}
//Encrypt using c = m^e mod n
static int rsaEncrypt(int m, int e, int n)
{
    int c = 1;

    for (int j = 0; j < e; j++)
    {
        c *= m;
        c %= n;
    }
    return c;
}
//Decrypt using m = c^d mod n
static int rsaDecrypt(int c, int d, int n)
{
    int m = 1;

    for (int j = 0; j < d; j++)
    {
        m *= c;
        m %= n;
    }
    return m;
}