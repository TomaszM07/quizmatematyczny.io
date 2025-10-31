using System;
using System.ComponentModel.Design;

namespace QuizMatematyczny
{
    class Program
    {
        static bool Podsumowanie(int suma_punktow)
        {
            Console.WriteLine($"Uzyskany wynik to: ({suma_punktow}/10)");
            double procent = suma_punktow / 10.0 * 100;
            Console.WriteLine($"Ilość procent wynosi: {procent}%");

            if (procent < 50)
                Console.WriteLine("Musisz poćwiczyć");
            else if (procent < 80)
                Console.WriteLine("Dobrze, ale możesz lepiej");
            else
                Console.WriteLine("Świetnie mistrzu matematyki");

            Console.WriteLine("Czy chcesz zagrać ponownie? 1 - tak, inne odpowiedzi to nie");
            int odpowiedz = Convert.ToInt32(Console.ReadLine());

            return odpowiedz == 1;


        }
        static int RozegrajQuiz(int min, int max, char[] operatory, bool potegi = false)
        {
            int suma_punktow = 0;

            for (int i = 0; i < 10; i++)
            {
                int liczba1 = LosowanieLiczb(min, max);
                int liczba2;
                 
                do
                {
                    liczba2 = LosowanieLiczb(min, max);
                } while (operatory.Contains('/') && liczba2 == 0);

                int potega1 = LosowanieLiczb(1, 11);
                int potega2 = LosowanieLiczb(1, 11);

                char operatorZnak = LosowanieZnakow(operatory);
                double wynikKomputer;
                string pytanie;

                switch (operatorZnak)
                {
                    case '+':
                        wynikKomputer = liczba1 + liczba2;
                        pytanie = $"{liczba1} + {liczba2}";
                        break;
                    case '-':
                        wynikKomputer = liczba1 - liczba2;
                        pytanie = $"{liczba1} - {liczba2}";
                        break;
                    case '*':
                        wynikKomputer = liczba1 * liczba2;
                        pytanie = $"{liczba1} * {liczba2}";
                        break;
                    case '/':
                        wynikKomputer = Math.Round((double)liczba1 / liczba2, 2);
                        pytanie = $"{liczba1} / {liczba2}";
                        break;
                    case '^':
                        if (potegi)
                        {
                            wynikKomputer = Math.Pow(potega1, potega2);
                            pytanie = $"{potega1} ^ {potega2}";
                        }
                        else
                        {
                            wynikKomputer = liczba1 + liczba2; // bezpieczna wartość
                            pytanie = "Błąd operatora";
                        }
                        break;
                    default:
                        wynikKomputer = 0;
                        pytanie = "Błąd operatora";
                        break;
                }

                Console.WriteLine("Rozwiąż równanie: " + pytanie);
                double odpowiedz = PobierzLiczbe();

                if (Math.Abs(odpowiedz - wynikKomputer) < 0.01)
                {
                    Console.WriteLine("Poprawna odpowiedź!");
                    suma_punktow++;
                }
                else
                {
                    Console.WriteLine($"Błędna odpowiedź. Poprawny wynik to: {wynikKomputer}");
                }
            }

            return suma_punktow;
        }

        static double PobierzLiczbe()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (double.TryParse(input, out double wynik))
                    return wynik;
                Console.WriteLine("Niepoprawna liczba, spróbuj ponownie:");
            }
        }

        static Random rnd = new Random();
        public static int LosowanieLiczb(int liczba1, int liczba2)
        {
            return rnd.Next(liczba1, liczba2);
        }

        public static char LosowanieZnakow(params char[] znaki)
        {
            if (znaki == null || znaki.Length == 0)
                throw new ArgumentException("Musisz podać przynajmniej jeden znak.");

            char operatorZnak = znaki[rnd.Next(znaki.Length)];

            return (char)operatorZnak;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w quizie matematycznym! Aby zacząć wciśnij enter: ");
            Console.ReadLine();

            while (true)
            {
                Console.WriteLine("Wybierz poziom trudności (1-łatwy, 2-średni, 3-trudny):");
                int poziomtrudnosci = (int)PobierzLiczbe();


                if (poziomtrudnosci == 1)
                {
                    int suma = RozegrajQuiz(1, 30, new char[] { '+', '-' });
                    bool kontynuuj = Podsumowanie(suma);
                    if (!kontynuuj) break;
                }

                if (poziomtrudnosci == 2)
                {
                    int suma = RozegrajQuiz(1, 41, new char[] { '+', '-', '*', '/' });
                    bool kontynuuj = Podsumowanie(suma);
                    if (!kontynuuj) break;
                }

                if (poziomtrudnosci == 3)
                {
                    int suma = RozegrajQuiz(1, 51, new char[] { '+', '-', '*', '/', '^' }, true);
                    bool kontynuuj = Podsumowanie(suma);
                    if (!kontynuuj) break;
                }



               



            }


        }

    }
}