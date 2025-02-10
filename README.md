# PhotoHub 

PhotoHub este o aplicație web care permite utilizatorilor să încarce, vizualizeze și organizeze imagini.
![image](https://github.com/user-attachments/assets/b340f017-5242-4392-90f4-cb2fcebcaca1)

## Descrierea Bazei de Date
![image](https://github.com/user-attachments/assets/6b18dba6-0dfa-4ff3-b718-e7e0960ffca1)

### Tabelul `Users`
Acest tabel păstrează informațiile esențiale despre utilizatori:
- `IdUser` (cheie primară): Identificator unic pentru utilizator.
- `Email`: Adresa de email a utilizatorului.
- `PasswordHash`: Parola criptată a utilizatorului.
- `Username`: Numele de utilizator ales.
- `Role`: Rolul utilizatorului.

### Tabelul `Images`
Stochează informațiile despre imaginile încărcate de utilizatori:
- `IdImage` (cheie primară): Identificator unic pentru fiecare imagine.
- `IdBlogPost` (cheie străină): Referință către postare.
- `Url`: URL catre imagine.

### Tabelul `BlogPosts`
Înregistrează albumele create de utilizatori:
- `IdBlogPost`: (cheie primară): Identificatorul unic a postarii.
- `Title` Titlul postului.
- `AuthorId`: (cheie străină): Referință către utilizatorul care a creat postarea.
- `CreatedDate` Data crearii.

### Tabelul `Comments`
Leagă imaginile de albumele în care sunt incluse:
- `IdComment` (cheie primară): Identificator unic pentru comentariu.
- `CreatedDate` Data crearii.
- `IdBlogPost` (cheie străină): Referință către postare.
- `IdUser` (cheie străină): Referință către autor.

## Tipuri de Utilizatori

### Utilizator
Un utilizator poate:
- Să se înregistreze și să se autentifice în aplicație.
- Să încarce și să vizualizeze imagini.
- Să scrie comentarii sub postari.

### Musafir
Un musafir poate:
- Să vizualizeze toate postarii si creatorii.
  
![image](https://github.com/user-attachments/assets/305eaffd-b688-42a4-9ac7-b2f2840daa85)

![image](https://github.com/user-attachments/assets/dde8930d-e945-416b-a554-f13df64d08a0)

## Fluxul de Utilizare pentru Utilizatori

1. **Crearea unui Cont și Autentificarea**
    - Utilizatorii se pot înregistra folosind o adresă de email validă sau se pot autentifica pentru a accesa contul personalizat.

2. **Încărcarea Imaginilor**
    - După autentificare, utilizatorii pot încărca imagini, care vor fi stocate în aplicație.

3. **Vizualizarea Imaginilor**
    - Utilizatorii pot vizualiza toate imaginile încărcate, inclusiv imagini din albume.

4. **Scrierea comentariilor sub postare**
    - Sub fiecare postare orice utilizator autentificat poate lasa un comentariu si il poate edita sau sterge.

![image](https://github.com/user-attachments/assets/79cb0719-6cb8-49cc-b0c6-d10098d78493)

![image](https://github.com/user-attachments/assets/6fdb69f5-ff05-4c98-bf13-d0ecfd274b39)

![image](https://github.com/user-attachments/assets/31c018f6-490c-442b-bcf7-46a02fc08a9f)

![image](https://github.com/user-attachments/assets/d9e2fda9-93a8-4227-9d46-521a85c9936a)

![image](https://github.com/user-attachments/assets/0aee243f-a15d-459b-ad32-73a4216ab08e)


