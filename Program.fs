open System

// Функция для чтения натурального числа с проверкой ввода
let rec readnatural (prompt: string) : int =
    printf "%s" prompt
    let input = Console.ReadLine()
    match Int32.TryParse(input) with
    | (true, n) when n > 0 ->
        if input = n.ToString() then
            n
        else
            printfn "Ошибка: число не должно начинаться с нуля."
            readnatural prompt
    | (true, _) ->
        printfn "Ошибка: число должно быть натуральным."
        readnatural prompt
    | (false, _) ->
        printfn "Ошибка: введено не число."
        readnatural prompt

// Функция для чтения одного символа с проверкой
let rec readchar (prompt: string) : char =
    printf "%s" prompt
    let input = Console.ReadLine()
    if String.IsNullOrEmpty(input) then
        printfn "Ошибка: введите хотя бы один символ."
        readchar prompt
    elif input.Length > 1 then
        printfn "Ошибка: введите ровно один символ."
        readchar prompt
    else
        input.[0]

// Функция для чтения строки с клавиатуры
let readstring (prompt: string) : string =
    printf "%s" prompt
    Console.ReadLine()

// Функция для добавления символа в начало каждой строки
let prependchar c str =
    sprintf "%c%s" c str

// Функция для создания последовательности строк с отложенным вычислением
let createSequence () =
    lazy (
        seq {
            let promptCount = "Введите кол-во строк для добавления в последовательность: "
            let count = readnatural promptCount
            for i in 1 .. count do
                let promptStr = sprintf "Введите %d-ю строку: " i
                yield readstring promptStr
        }
    )

// Основная функция
[<EntryPoint>]
let main argv =
    printfn "программа для добавления символа в начало каждой строки"
    printfn "Вводите строки:"

    // Сначала запрашиваем ввод строк (lazy-последовательность будет вычислена сейчас):
    let lines = createSequence().Value |> Seq.toList

    // Затем запрашиваем символ, который нужно добавить к каждой строке:
    let charToPrepend = readchar "Введите символ, который нужно добавить в начало каждой строки: "

    // Создаем новый список, используя List.map, который добавляет символ в начало каждой строки:
    let newLines = List.map (prependchar charToPrepend) lines

    printfn "\nНовая последовательность строк (вычисляется лениво):"
    newLines |> List.iter (printfn "%s")

    printf "\n"
    Console.ReadKey() |> ignore
    0