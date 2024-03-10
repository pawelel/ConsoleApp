## Uwagi

### Async/Await i I/O Bound Operations

- Operacje I/O, takie jak czytanie plików, mogą korzystać z asynchroniczności, aby lepiej zarządzać zasobami.

### Obsługa wyjątków

- W kodzie brakuje obsługi wyjątków, co może prowadzić do nieprzewidzianego zachowania aplikacji.
Można użyć bloków try/catch, aby złapać potencjalne wyjątki, np. gdy plik nie istnieje lub występują problemy z dostępem do pliku.

### Poprawka na pętlę for

- W obecnej pętli for (w ImportAndPrintData), istnieje błąd indeksowania (`i <= importedLines.Count`).
Powinno być `i < importedLines.Count`, aby uniknąć `IndexOutOfRangeException`.

### Unikanie wielokrotnego wywoływania Count()

- W obecnej formie, `ImportedObjects.Count()` jest wywoływane wielokrotnie w pętli, co jest nieefektywne.
Zalecane jest zapisanie wyniku do zmiennej przed rozpoczęciem pętli.

### Optymalizacja logiki szukania obiektów

- Aktualna logika przeglądania kolekcji ImportedObjects jest nieefektywna, ponieważ wielokrotnie przechodzi przez całą kolekcję.					
Dla dużych kolekcji, zalecane jest zastosowanie odpowiednich struktur danych, takich jak np. słowniki lub grupy, aby zoptymalizować wyszukiwanie.

### Deklaracje właściwości w klasie ImportedObject

- Właściwości:
  - `Schema`, 
  - `ParentName`, 
  - `ParentType`, 
  - `DataType`, 
  - `IsNullable` 
  - `NumberOfChildren` 
mogą być zdefiniowane jako właściwości z publicznym getterem i setterem, tak jak `Name` i `Type`, aby zachować spójność i kapsułkowanie.

### Podwójne użycie tej samej właściwości

- W klasie `ImportedObject` właściwość `Name` jest zadeklarowana, mimo że jest dziedziczona z klasy `ImportedObjectBaseClass`.
Jeśli nie jest to ceolowe działanie, zalecane jest usunięcie tej deklaracji.

### Unikanie wycieków pamięci i zasobów

- Aby automatycznie zwolnić zasoby, warto użyć bloku `using` do zarządzania zasobami, takimi jak `StreamReader` i `StreamWriter`.

### Logowanie zamiast Console.WriteLine

- Zamiast używać `Console.WriteLine`, zalecane jest użycie bibliotek do logowania, takich jak `Serilog`, `NET` lub `NLog`, aby zapewnić bardziej elastyczne i konfigurowalne logowanie.

### Unikanie wielokrotnego zagnieżdżania się pętli

- W kodzie występuje zagnieżdżenie się pętli, co może prowadzić do złożoności obliczeniowej O(n^2).

### Brak walidacji danych wejściowych

- Dane nie są weryfikowane pod kątem poprawności, co może prowadzić do nieprzewidzianego zachowania aplikacji.

### Nadmiarowe tworzenie obiektu `ImportedObject`

- W metodzie `ImportAndPrintData` tworzony jest obiekt `ImportedObject` i dodawany do listy `ImportedObjects`:
`ImportedObjects = new List<ImportedObject>() { new ImportedObject() };`, co powoduje błędy null reference, ponieważ odwołanie do `ImportedObjects` jest później nadpisane.

### Zbędnie użyte printData w metodzie `ImportAndPrintData`

- YAGNI - You Ain't Gonna Need It. W metodzie `ImportAndPrintData` jest zmienna `printData`, która nie jest używana.

### Brak rozdzielenia na import i print

- Metoda `ImportAndPrintData` wykonuje dwie operacje: import i print. Zalecane jest rozdzielenie tych operacji na dwie osobne metody.

### Nazwy zmiennych wewnętrznych

- Zalecane jest stosowanie konwencji camelCase dla zmiennych lokalnych, np. `_importedObjects` lub `importedObjects` zamiast `ImportedObjects`.

## Spostrzeżenia

- magic strings z błędami pisowni
- brak obsługi błędów
- brak testów
- brak dokumentacji-co aplikacja robi, jak jej używać, jakie są wymagania
- brakujące modyfikatory dostępu

## Sugestie

- aplikacja mogłaby mieć nazwę sugerującą jej przeznaczenie, np.`DataStructureVisualizer`.
- Warto rozważyć użycie typów wyliczeniowych dla `Type`, `ParentType`, `DataType` i bool dla `IsNullable`, zamiast używania stringów, aby zwiększyć czytelność i bezpieczeństwo kodu.
