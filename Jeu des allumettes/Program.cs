using System;
using System.Linq;

namespace Jeu_des_allumettes
{
    class Program
    {
        static void Main(string[] args)
        {
            int longueur_de_la_base;
            int ia;

            /* Envoie un message à l'utilisateur puis
             prend une entrée déterminant la longueur de la base de la pyramide.
             Cette dernière est égale à son nombre de colonnes.
             Boucle si l'entrée n'est pas valable. */

            do
            {
                //Console.Clear();
                Console.WriteLine("Jeu de l'allumette \n \nChoisissez la taille de votre base (5 minimum, doit être un chiffre impaire).");
            } while (!int.TryParse(Console.ReadLine(), out longueur_de_la_base) || longueur_de_la_base < 5 || longueur_de_la_base % 2 == 0);

            Console.WriteLine("La base est longue de: " + longueur_de_la_base);

            Console.WriteLine("\n Choisissez la difficulté \n 1- Facile \n 2- Normal \n 3- PVP");

            while (!int.TryParse(Console.ReadLine(), out ia) || ia < 1 || ia > 3)
            {
                Console.Clear();
                Console.WriteLine("Choisissez la difficulté \n 1- Facile \n 2- Normal \n 3- PVP");

            }
            //Console.Clear();

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

            int[] player_inputs = null;

            while (game_over == false)
            {
                if (tableau.Sum() == 0 && game_over == false)
                {
                    game_over = true;
                    Console.WriteLine("J1 a gagné !");
                }
                else if (game_over == false)
                {
                    build_pyramide(longueur_de_la_base, tableau);
                    player_inputs = player_turn(tableau);
                    Console.Clear();
                }
                if (tableau.Sum() == 0 && game_over == false)
                {
                    game_over = true;
                    Console.WriteLine("\n \n  J2 a gagné !");
                }
                else if (game_over == false)
                {
                    build_pyramide(longueur_de_la_base, tableau);
                    if (ia == 1)
                    {
                        ia_turn(tableau);
                    }
                    else  if (ia == 2)
                    {
                        ia_turn2(tableau, player_inputs[0], player_inputs[1], player_inputs[2]);
                    }
                    else
                    {
                        player_turn(tableau);
                    }
                    
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

        private static int[] player_turn(int[] tableau)
        {
            Console.WriteLine("\n Tour du joueur \n");

            int ligne_joueur;
            int nombre_joueur;
            do                                  // Envoie un message à l'utilisateur puis
                                                // prend une entrée déterminant sur quelle ligne enlever des allumettes. 
                                                // Boucle si l'entrée n'est pas valable
            {
                Console.WriteLine("Choisissez la ligne sur laquelle enlever des allumettes.");
            } while (!int.TryParse(Console.ReadLine(), out ligne_joueur) || ligne_joueur <= 0 || ligne_joueur >= tableau.Length + 1 || tableau[ligne_joueur - 1] == 0);

            ligne_joueur = ligne_joueur - 1;

            do                                  // Envoie un message à l'utilisateur puis
                                                // prend une entrée déterminant le nombre d'allumettes à enlever sur la ligne choisie. 
                                                // Boucle si l'entrée n'est pas valable
            {
                Console.WriteLine("Choisissez le nombre d'allumettes à enlever, il ne doit pas être inférieur à un ni supérieur au nombre d'allumettes sur la ligne.");
            } while (!int.TryParse(Console.ReadLine(), out nombre_joueur) || nombre_joueur < 1 || nombre_joueur > tableau[ligne_joueur]);

            int last_line = tableau[ligne_joueur];
            tableau[ligne_joueur] = tableau[ligne_joueur] - nombre_joueur;

            int[] player_inputs = new int[3];
            player_inputs[0] = ligne_joueur;
            player_inputs[1] = nombre_joueur;
            player_inputs[2] = last_line;
            return player_inputs;
        }

        private static void ia_turn(int[] tableau)
        {
            Console.WriteLine("\n Tour de l'IA \n");

            Random ligne_random = new Random();
            int ligne_ia = ligne_random.Next(0, tableau.Length);

            while (tableau[ligne_ia] == 0)
            {
                ligne_ia = ligne_random.Next(0, tableau.Length);
            }

            Random nombre_random = new Random();
            int nombre_ia = nombre_random.Next(1, tableau[ligne_ia]);

            tableau[ligne_ia] = tableau[ligne_ia] - nombre_ia;
        }
        private static void ia_turn2(int[] tableau, int ligne_joueur, int nombre_joueur, int last_line)
        {
            Console.WriteLine("\n Tour de l'IA \n");

            // Vérifie s'il ne reste plus que deux lignes dans le tableau et si l'une de ces lignes est égale à 1
            int x = 0;
            int lignes_restantes = 0;
            int ligne_grande = 0;
            bool ligne_une = false;
            while (x != tableau.Length)
            {
               
                if (tableau[x] > 1)
                {                    
                    ++lignes_restantes;
                    ligne_grande = x;
                }
                if (tableau[x] == 1)
                {
                    ++lignes_restantes;
                    ligne_une = true;
                }
                ++x;
            }

            Console.WriteLine(lignes_restantes);
            
            // Si le joueur a enlevé toute les allumettes, ou toutes sauf une, d'une ligne lors de son tour, 
            // choisit une autre ligne random et la décrémente d'un nombre random

            if (nombre_joueur == last_line || nombre_joueur == last_line - 1)
            {
                              
                // choisi une ligne du tableau qui n'est pas égale à 0 de façon aléatoire
                Random ligne_random = new Random();
                int ligne_ia = ligne_random.Next(0, tableau.Length);

                while (tableau[ligne_ia] == 0)
                {
                    ligne_ia = ligne_random.Next(0, tableau.Length);
                }

                Random nombre_random = new Random();
                
                int nombre_ia = 0;


                if (lignes_restantes == 2)
                {
                    if (ligne_une == true)
                    {
                        ligne_ia = ligne_grande;                       
                    }
                    else
                    {
                        nombre_ia = 2;
                    }
                    
                }

                tableau[ligne_ia] = nombre_ia;
            }
            else
            {
                tableau[ligne_joueur] = 1;
            }
        }
        private static void ia_turn3(int[] tableau)
        {

        }
    }
}
         