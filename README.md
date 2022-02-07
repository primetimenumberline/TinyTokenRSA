# TinyTokenRSA

Teaching myself RSA using the most naive implementation I can.

1)
Right off the bat I had to upgrade from the direct method to the memory efficient implementation for rsaEncrypt and rsaDecrypt due to overflow, see: 
https://en.wikipedia.org/wiki/Modular_exponentiation
What if I would have done the following and called it:
int rsaEncrypt2(int m, int e, int n)
{
    int c = (m ^ e) % n;
    return c;
}

2)
I did character by character calls to rsaEncrypt and rsaDecrypt. In reality, I believe the decimal representation of the message is concatenated and only run through the functions ONCE. There is also padding and such involved.
Performing a char by char approach to encryption, leaves the ciphered message vulnerable to frequncy analysis. https://en.wikipedia.org/wiki/Frequency_analysis
For instance, in the English language, the character 'e' shows up a lot in our language ... data captures could be done over a long period in order to start detecting these weaknesses in transmissions

3)
Missing the following concept but I think it's important to note in the readme
https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test
