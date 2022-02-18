using Tarefas.db;

bool sair = false;
while (!sair)
{
    string opcao = UI.SelecionaOpcaoEmMenu();

    switch (opcao)
    {
        case "L": ListarTodasAsTarefas(); break;
        case "P": ListarTarefasPendentes(); break;
        case "I": ListarTarefasPorId(); break;
        case "D": ListarTarefasPorDescricao(); break;
        case "N": IncluirNovaTarefa(); break;
        case "A": AlterarDescricaoDaTarefa(); break;
        case "C": ConcluirTarefa(); break;
        case "E": ExcluirTarefa(); break;

        case "S":
            sair = true;
            break;

        default:
            UI.ExibeErro("\nOpção não reconhecida.");
            break;
    }

    Console.Write("\nPressione uma tecla para continuar...");
    Console.ReadKey();
}

void ListarTodasAsTarefas()
{
    UI.ExibeDestaque("\n-- Listar todas as Tarefas ---");

    // Continue daqui
    using (var _db = new tarefasContext())
    {
        var tarefas = _db.Tarefa.ToList<Tarefa>();

        Console.WriteLine();
        foreach (var tarefa in tarefas)
        {
            string concluida = tarefa.Concluida ? "X" : " ";
            Console.WriteLine($"[{concluida}] {tarefa.Id} => {tarefa.Descricao}");
        }

        int quantidadeTarefas = tarefas.Count();
        Console.WriteLine($"\nEncontrados {quantidadeTarefas} tarefa(s).");
    }
}

void ListarTarefasPendentes()
{
    UI.ExibeDestaque("\n-- Listar Tarefas Pendentes ---");

    // Continue daqui
    using (var _db = new tarefasContext())
    {
        var tarefasPendentes = _db.Tarefa
            .Where(t => !t.Concluida)
            .OrderByDescending(t => t.Id)
            .ToList<Tarefa>();

        Console.WriteLine();
        foreach (var tarefa in tarefasPendentes)
        {
            string concluida = tarefa.Concluida ? "X" : " ";
            Console.WriteLine($"[{concluida}] {tarefa.Id} => {tarefa.Descricao}");
        }

        int quantidadeTarefas = tarefasPendentes.Count();
        Console.WriteLine($"\nEncontrados {quantidadeTarefas} tarefa(s).");
    }
}

void ListarTarefasPorDescricao()
{
    UI.ExibeDestaque("\n-- Listar Tarefas por Descrição ---");
    string descricao = UI.SelecionaDescricao();

    // Continue daqui
    using (var _db = new tarefasContext())
    {
        var tarefas = _db.Tarefa
            .Where(t => t.Descricao.Contains(descricao))
            .OrderBy(t => t.Id)
            .ToList<Tarefa>();

        Console.WriteLine();
        foreach (var tarefa in tarefas)
        {
            string concluida = tarefa.Concluida ? "X" : " ";
            Console.WriteLine($"[{concluida}] {tarefa.Id} => {tarefa.Descricao}");
        }

        int quantidadeTarefas = tarefas.Count();
        Console.WriteLine($"\nEncontrados {quantidadeTarefas} tarefa(s).");
    }
}

void ListarTarefasPorId()
{
    UI.ExibeDestaque("\n-- Listar Tarefas por Id ---");
    int id = UI.SelecionaId();

    // Continue daqui
    using (var _db = new tarefasContext())
    {
        var tarefa = _db.Tarefa.Find(id);

        if (tarefa == null)
        {
            Console.WriteLine("\nTarefa não encontrada.");
            return;
        }

        string concluida = tarefa.Concluida ? "X" : " ";
        Console.WriteLine($"\n[{concluida}] {tarefa.Id} => {tarefa.Descricao}");
    }
}

void IncluirNovaTarefa()
{
    UI.ExibeDestaque("\n-- Incluir Nova Tarefa ---");
    string descricao = UI.SelecionaDescricao();

    // Continue daqui
    if (String.IsNullOrEmpty(descricao))
    {
        Console.WriteLine("\nNão é possível cadastrar tarefa sem descrição.");
        return;
    }

    using (var _db = new tarefasContext())
    {
        var tarefa = new Tarefa
        {
            Descricao = descricao
        };

        _db.Tarefa.Add(tarefa);
        _db.SaveChanges();

        string concluida = tarefa.Concluida ? "X" : " ";
        Console.WriteLine($"\n[{concluida}] {tarefa.Id} => {tarefa.Descricao}");
    }
}

void AlterarDescricaoDaTarefa()
{
    UI.ExibeDestaque("\n-- Alterar Descrição da Tarefa ---");
    int id = UI.SelecionaId();
    string descricao = UI.SelecionaDescricao();

    // Continue daqui
    if (String.IsNullOrEmpty(descricao))
    {
        Console.WriteLine("\nNão é possível deixar tarefa sem descrição.");
        return;
    }

    using (var _db = new tarefasContext())
    {
        var tarefa = _db.Tarefa.Find(id);

        if (tarefa == null)
        {
            Console.WriteLine("\nTarefa não encontrada.");
            return;
        }

        tarefa.Descricao = descricao;
        _db.SaveChanges();

        string concluida = tarefa.Concluida ? "X" : " ";
        Console.WriteLine($"\n[{concluida}] {tarefa.Id} => {tarefa.Descricao}");
    }
}

void ConcluirTarefa()
{
    UI.ExibeDestaque("\n-- Concluir Tarefa ---");
    int id = UI.SelecionaId();

    // Continue daqui
    using (var _db = new tarefasContext())
    {
        var tarefa = _db.Tarefa.Find(id);

        if (tarefa == null)
        {
            Console.WriteLine("\nTarefa não encontrada.");
            return;
        }

        if (tarefa.Concluida)
        {
            Console.WriteLine("\nTarefa concluída anteriormente.");
            return;
        }

        tarefa.Concluida = true;
        _db.SaveChanges();

        string concluida = tarefa.Concluida ? "X" : " ";
        Console.WriteLine($"\n[{concluida}] {tarefa.Id} => {tarefa.Descricao}");
    }
}

void ExcluirTarefa()
{
    UI.ExibeDestaque("\n-- Excluir Tarefa ---");
    int id = UI.SelecionaId();

    // Continue daqui
    using (var _db = new tarefasContext())
    {
        var tarefa = _db.Tarefa.Find(id);

        if (tarefa == null)
        {
            Console.WriteLine("\nTarefa não encontrada.");
            return;
        }

        _db.Remove(tarefa);
        _db.SaveChanges();

        Console.WriteLine("\nTarefa excluída com sucesso.");
    }
}
