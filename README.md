# Haruka3DPrinting
![logo](https://user-images.githubusercontent.com/75331740/217664821-7811e6f0-772c-4732-9b84-7364ddc6ce25.png)

Haruki 3D Printing este un magazin online cu obiecte printate 3D. In cadrul aplicatiei userul are acces in a vizualiza mai multe categorii de produse (standard, produse cu licheni, produse personalizate, produse pentru caini si produse custom). Un produs poate fi achizitionat doar daca clientul este logat. 
Un client logat are acces la pagina "Contul meu" unde isi poate vedea detaliile, comenzile si statusul lor, modifica numarul de telefon, si lasa feedback. De asemenea poate vizualiza produsele si adauga in cos ceea ce doreste sa achizitioneze. Atunci cand considera ca doreste sa plaseze comanda, acesta poate face asta accesand cosul de cumparaturi si apasa butonul Plaseaza comanda.
Aplicatia poate fi utilizata si de angajati (executant/proiectant) pentru a gestiona comenzile si imprimantele. Acesta in functie de rol, poate schimba statusul unei imprimante pentru ca alti angajati sa stie ce imprimante sunt disponbile sau nu pentru a confectiona alte produse. De asemenea un angajat poate schimba statusul comenzilor si vedea detalii despre ea

Cerinte:
* Sa aiba mai multe rute:
	> ruta principala este ['home'] care ne duce la pagina principala unde se poate vedea logo-ul si produsele in functie de categorie. Din home avem acces spre mai multe pagini in functie de rol. 
        > In ['cont'] avem componentele de login si register. In navbar avem acces atat la login cat si register dar poti accesa register din pagina de login si invers. 
	> In ['client'] avem 2 componente: pagina de cont client si cosul de cumparaturi.
	> In ['gestiune'] avem mai multe componente precum comenzi,  gestiune, imprimante, executant si proiectant. De asemenea avem o pagina in care pentru a fi siguri ca cel care acceseaza pagina de angajati este chiar un angajat acesta trebuie sa introduca o parola cunsocuta din momentul angajarii(salvata in baza de date sub forma criptata). In functie de rol avem pagina de executant care are acces la paginile comenzi si detalii imprimante si proiectant care are acces doar la pagina de comenzi. 

* Sa se foloseasca componente reutilizabile: componente reutilizabila folosite sunt navbarul (NavbarComponent) si componenta de produse (ProduseComponent) care se foloseste o data pentru fiecare carte din librarie

* Sa se comunice intre componente: se folosesc toate cele 3 tipuri de comunicare

      > Comunicarea prin serviciu (intre componente fara nicio legatura copil-parinte): este des folosita pentru a lega componente precum login register de homeComponent. De asemnea pentru a te redirectiona comenzide pagina detalii comanda sau de a te redirectiona dupa plasarea unei comenzi inapoi la home.

      > @Input si @Output pentru comunicarea dintre componenta parinte si componenta copil (si invers): este folosita pentru a lega componenta copil produs de componenta parinte home.


* Rute publice si private: ca ruta publica avem pagina de home unde toti au accessi paginile de login/register. Cele private sunt cele in care utilizatorul nu poate intra daca nu are cont, pentru asta este folosit un guard care afiseaza un alert si ne redirectioneaza la pagina de login (poza mai jos).
 
 ![guard](https://user-images.githubusercontent.com/75331740/217665663-2bf47395-8612-43b8-9a4c-1d0cf2bda1cf.jpg)

 

* Sa fie cel putin o pagina cu un form (login/register)
form login
form register
mini form schimbare numar de telefon
mini form pagina angajatilor
Firebase sau orice alt mediu de backend: backendul este cel creat cu .NET si adaugat in repo.
