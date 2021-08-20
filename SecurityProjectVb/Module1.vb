Imports SecurityHelpers

Module Module1

    Sub Main()
        Dim CryptoManager As SymmetricCryptographyHelpers = New SymmetricCryptographyHelpers()
        Dim Key As (String, String) = CryptoManager.GenerateSymmetricEncryptionKey()
        Dim generatedKey = Key.Item1
        Dim generatedIV = Key.Item2

        Dim unencryptedText As String = "Test"
        Dim encryptedText As String = CryptoManager.EncryptString(unencryptedText, generatedIV, generatedKey)
        Dim decryptedText As String = CryptoManager.DecryptString(encryptedText, generatedIV, generatedKey)

        Console.WriteLine($"key: {generatedKey}")
        Console.WriteLine($"iv: {generatedIV}")

        Console.WriteLine($"Encrypted text: {encryptedText}")
        Console.WriteLine($"Decrypted text: {decryptedText}")

        Console.ReadLine()


    End Sub

End Module
