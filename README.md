# NetPC_zadanie

Aplikacja webowa do zarządzania kontaktami z uwierzytelnianiem użytkowników.

## 📚 Opis projektu

Aplikacja pozwala na:
- Rejestrację użytkowników.
- Logowanie z generowaniem tokena JWT.
- Zarządzanie kontaktami (dodawanie, edycja, usuwanie, przeglądanie listy kontaktów).
- Obsługę ról i uwierzytelnienia przy użyciu JWT.

## 🏗 Struktura katalogów

| Katalog / Plik                      | Opis                                 |
|-------------------------------------|--------------------------------------|
| `Controllers/`                      | Kontrolery API                       |
| ├── `AuthController.cs`             | Obsługa logowania i rejestracji      |
| ├── `ContactsController.cs`         | CRUD dla kontaktów                   |
| └── `UserController.cs`             | Operacje na użytkownikach            |
| `Data/`                             | Konfiguracja bazy danych             |
| ├── `DataContext.cs`                | Kontekst EF Core                     |
| ├── `DataInitializer.cs`            | Inicjalizacja danych                 |
| `DTO/`                              | Klasy DTO                            |
| ├── `ContactDTO.cs`                 | DTO kontaktu                         |
| └── `RegisterUserDTO.cs`            | DTO rejestracji użytkownika          |
| `Interface/`                        | Interfejsy repozytoriów              |
| ├── `IContactRepository.cs`         | Interfejs repozytorium kontaktów     |
| └── `IUserRepository.cs`            | Interfejs repozytorium użytkowników  |
| `Migrations/`                       | Pliki migracji bazy danych           |
| ├── `InitialCreate.cs`              | Pierwsza migracja                    |
| ├── `NowaMigracja.cs`               | Dodatkowe zmiany                     |
| └── `DataContextModelSnapshot.cs`   | Snapshot bazy danych                 |
| `Models/`                           | Modele domenowe                      |
| ├── `Contact.cs`                    | Model kontaktu                       |
| └── `User.cs`                       | Model użytkownika                    |
| `Repositories/`                     | Implementacje repozytoriów           |
| ├── `ContactRepository.cs`          | Repozytorium kontaktów               |
| └── `UserRepository.cs`             | Repozytorium użytkowników            |
| `Services/`                         | Warstwa logiki biznesowej            |
| ├── `AuthService.cs`                | Obsługa JWT i autoryzacji            |
| ├── `ContactService.cs`             | Logika kontaktów                     |
| ├── `PasswordService.cs`            | Haszowanie i weryfikacja haseł       |
| └── `UserService.cs`                | Logika użytkowników                  |
| `appsettings.json`                  | Konfiguracja aplikacji               |
| `netpc.db`                          | Plik bazy danych (lokalny)           |
| `NetPC_zadanie.csproj`             | Plik projektu .NET                   |
| `NetPC_zadanie.csproj.Backup`      | Backup pliku projektu                |
| `NetPC_zadanie.http`               | Testowe zapytania HTTP               |
| `Program.cs`                        | Główny plik uruchomieniowy           |

## 📦 Wykorzystane biblioteki

- `Microsoft.AspNetCore.Authentication.JwtBearer` – obsługa JWT.
- `Microsoft.EntityFrameworkCore` – ORM dla bazy danych.
- `Microsoft.EntityFrameworkCore.SqlServer` – provider SQL Server.
- `AutoMapper` – mapowanie obiektów DTO ↔ Model.
- `Swashbuckle.AspNetCore` – dokumentacja Swagger UI.

## 🔐 Uwierzytelnianie i autoryzacja

- JWT generowany i przechowywany w `localStorage`.
- Autoryzacja kontrolowana za pomocą `[Authorize]`.

## ⚙️ Kompilacja i uruchomienie

### Backend
1. Przywróć zależności NuGet:
    ```
    dotnet restore
    ```
2. Wykonaj migracje bazy danych:
    ```
    dotnet ef database update
    ```
3. Uruchom aplikację:
    ```
    dotnet run
    ```
4. Domyślny adres: `https://localhost:7138`

### Frontend (Angular)
1. Otwórz folder z aplikacją frontendową.
2. Zainstaluj zależności:
    ```
    npm install
    ```
3. Uruchom Angular:
    ```
    ng serve
    ```
4. Domyślny adres: `http://localhost:4200`

## 📄 Uwagi

- W trybie developerskim token JWT przechowywany jest w `localStorage`. W środowisku produkcyjnym rekomendowane jest użycie ciasteczek HttpOnly.
- Hasła hashowane przy użyciu `Rfc2898DeriveBytes` (PBKDF2).
- `withCredentials: true` w Angularze dla obsługi ciasteczek.
