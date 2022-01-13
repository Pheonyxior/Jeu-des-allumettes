using System;
using System.Linq;

namespace Jeu_des_allumettes
{
    class Program
    {
        static void Main(string[] args)
        {
            int longueur_de_la_base;

            /* Envoie un message à l'utilisateur puis
             prend une entrée déterminant la longueur de la base de la pyramide.
             Cette dernière est égale à son nombre de colonnes.
             Boucle si l'entrée n'est pas valable. */

            do
            {
                Console.Clear();
                Console.WriteLine("Jeu de l'allumette \n \nChoisissez la taille de votre base (5 minimum, doit être un chiffre impaire).");
            } while (!int.TryParse(Console.ReadLine(), out longueur_de_la_base) || longueur_de_la_base < 5 || longueur_de_la_base % 2 == 0);


            Console.WriteLine("La base est longue de: " + longueur_de_la_base);
            
            int x = 0;
            int i = longueur_de_la_base;
            int[] tableau = new int[(longueur_de_la_base / 2) + 1];
            bool game_over = false;

            // Boucle qui détermine le nombre d'allumettes à chaque lignes
            // de la pyramides en fonction de la valeur de la longueur de base.
            // à chaque itération de la boucle : décrémente i de 2 ; 
            // la ligne du tableau x est incrémenter par i ; 
            // passe à la prochaine ligne en incrémentent x de 1.

            while (x != tableau.Length)                      
            {
                tableau[x] = i;
                i = i - 2;
                x = x + 1;
            }

            // Si le total d'allumettes du tableau est égal à zéro, game_over devient true.
            // L'ia et le joueur ne peuvent jouer, et la condition de victoire ne peut être check  
            // que si game_over est false.

            while (game_over == false)
            {
                if (tableau.Sum() == 0 && game_over == false)
                {
                    game_over = true;                    
                    Console.WriteLine("Tu as gagné !");
                }
                else if (game_over == false)
                {                    
                    build_pyramide(longueur_de_la_base, tableau); 
                    player_turn(tableau);
                    Console.Clear();
                }
                if (tableau.Sum() == 0 && game_over == false)
                {
                    game_over = true;                    
                    Console.WriteLine("Tu as perdu !");
                }
                else if (game_over == false)
                {
                    
                    build_pyramide(longueur_de_la_base, tableau); 
                    ia_turn(tableau);
                }
            }
        }

        // Créer une pyramide d'allumettes 'I' avec {longueur de la base} colonnes et {tableau.Lenght} lignes.
        // Créer un cadre de '-' et de '|' autour de la pyramide, ainsi que numérote chaque ligne.
       
        private static void build_pyramide(int longueur_de_la_base, int[] tableau)
        {            
            print_planches(longueur_de_la_base);

            print_etages(longueur_de_la_base, tableau);

            print_planches(longueur_de_la_base);
        }

        // Créer une ligne de "-" égale à la longueur de la base
        // plus de l'espace pour plus de clarté
       
        private static void print_planches(int longueur_de_la_base)     
        {
            Console.Write("  \t");
            int i = longueur_de_la_base + 4;
            while (i != 0)
            {
                Console.Write("-");
                i = i - 1;
            }
            Console.Write("\n");
        }

        // Créer les étages d'allumettes "I" ainsi que les espaces vides et les "|" au début et à la fin
        // Numérote chaque lignes en ajoutant 1 à la valeure sur le tableau pour chacune pour partir de 1 au lieu de 0
      
        private static void print_etages(int longueur_de_la_base, int[] tableau)            
        {
            int x = tableau.Length - 1;

            while (x != -1)
            {
                int spaces = longueur_de_la_base + 2 - tableau[x];
                int i = spaces;
                int z = tableau[x];
               
                Console.Write((x + 1) + "\t|");

                // Boucle tant que le nombres d'espaces à faire n'a pas été atteint.
                // Quand i est égal à la moitié du nombre d'espaces, print le nombre d'allumette
                // de la ligne du tableau, avant de reprint des espaces

                while (i != 0)                         
                {
                    Console.Write(" ");
                    i = i - 1;
                    
                    if (i == spaces / 2)
                    {
                        while (z != 0)
                        {
                            Console.Write("I");
                            z = z - 1;
                        }
                    }
                }
                Console.Write("|\n");
                x = x - 1;
            }
        }
       
        private static void player_turn(int[] tableau)
        {
            Console.WriteLine("\n Tour du joueur \n");

            int ligne_choisie;
            int nombre_choisi;
            do                                  // Envoie un message à l'utilisateur puis
                                                // prend une entrée déterminant sur quelle ligne enlever des allumettes. 
                                                // Boucle si l'entrée n'est pas valable
            {
                Console.WriteLine("Choisissez la ligne sur laquelle enlever des allumettes.");
            } while (!int.TryParse(Console.ReadLine(), out ligne_choisie) || ligne_choisie <= 0 || ligne_choisie >= tableau.Length +1 || tableau[ligne_choisie - 1] == 0); 
            
            ligne_choisie = ligne_choisie - 1;
            
            do                                  // Envoie un message à l'utilisateur puis
                                                // prend une entrée déterminant le nombre d'allumettes à enlever sur la ligne choisie. 
                                                // Boucle si l'entrée n'est pas valable
            {
                Console.WriteLine("Choisissez le nombre d'allumettes à enlever, il ne doit pas être inférieur à un ni supérieur au nombre d'allumettes sur la ligne.");
            } while (!int.TryParse(Console.ReadLine(), out nombre_choisi) || nombre_choisi < 1 || nombre_choisi > tableau[ligne_choisie]);

            tableau[ligne_choisie] = tableau[ligne_choisie] - nombre_choisi;          
        }

        private static void ia_turn(int[] tableau)
        {
            Console.WriteLine("\n Tour de l'IA \n");
            
            Random ligne_random = new Random();
            int ligne_choisie = ligne_random.Next(0, tableau.Length);

            while (tableau[ligne_choisie] == 0)
            { 
            ligne_choisie = ligne_random.Next(0, tableau.Length);
            }

            Random nombre_random = new Random();
            int nombre_choisi = nombre_random.Next(1, tableau[ligne_choisie]);

            tableau[ligne_choisie] = tableau[ligne_choisie] - nombre_choisi;
        }

    }
}

                 
   
         