# BlogCMS

Prosty system zarządzania treścią (CMS) dla bloga. Zbudowany na .NET 8 (Web API)
z wykorzystaniem wzorca Repository i Entity Framework Core (SQL Server).

API pozwala na pełen CRUD na postach: dodawanie, odczyt, edycję i usuwanie.

## Wymagania

- .NET 8 SDK
- SQL Server (np. LocalDB, SQL Server Express albo pełny SQL Server)

## Jak uruchomić

1. Sklonuj repozytorium i wejdź do katalogu:
   ```
   git clone <adres-repo>
   cd BlogCMS
   ```

2. Sprawdź connection string w `appsettings.json` (klucz `DefaultConnection`)
   i ewentualnie dostosuj go do swojej instancji SQL Server.

3. Pobierz pakiety:
   ```
   dotnet restore
   ```

4. Utwórz bazę danych na podstawie migracji:
   ```
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
   (jeśli nie masz narzędzia `dotnet ef`, zainstaluj je: `dotnet tool install --global dotnet-ef`)

5. Uruchom aplikację:
   ```
   dotnet run
   ```

6. Wejdź na Swagger UI w przeglądarce, np.:
   ```
   https://localhost:5001/swagger
   ```

## Endpointy

| Metoda | Adres             | Opis                      |
|--------|-------------------|---------------------------|
| GET    | /api/posts        | Pobierz wszystkie posty   |
| GET    | /api/posts/{id}   | Pobierz post po Id        |
| POST   | /api/posts        | Dodaj nowy post           |
| PUT    | /api/posts/{id}   | Zaktualizuj post          |
| DELETE | /api/posts/{id}   | Usuń post                 |

## Przykładowy post (body do POST/PUT)

```json
{
  "id": 0,
  "title": "Pierwszy post",
  "content": "Treść mojego pierwszego posta",
  "imageUrl": "https://example.com/obrazek.png",
  "published": "2025-01-01T12:00:00"
}
```
