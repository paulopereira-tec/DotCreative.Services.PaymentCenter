# Payment Center
O projeto **PaymentCenter** é uma iniciativa *open source* com o objetivo de criar uma ferramenta que padronize a utilização de plataformas de pagamentos e bancos utilizadas no mercado.

## Histórico
Atualmente, entre as atuais plataformas de pagamento e bancos do mercado não existe nenhuma padronização na forma com que é feita a geração da cobrança, consulta e listagem das mesmas.

Cada banco e cada plataforma de pagamento tem seus meios e métodos de implementação de cobranças. Consequentemente, isso dificulta a implementação concorrente de diversos meios de pagamento ao mesmo tempo.

Este projeto foi pensado para ser *open source*. Ajude na implementação de novas plataformas e melhorias.

## Plataformas já integradas

- *Paghiper:* Boleto e PIX.

## Entenda como funciona
A forma da implementação é bem simples e pode ser visualizada no projeto de testes. Cada plataforma poderá ser implementada separadamente ou em conjunto. As regras abaixo seguirá igualmente para todas as plataformas.

Inicie preparando a transação conforme os exemplos abaixo:

```
// Número de sua ordem de pagamento. Número que corresponda a uma identificação própria sua.
string orderId = Guid.NewGuid().ToString();

// Data de vencimento da sua cobrança
DateTime dueDate = DateTime.Now.AddDays(15);

// Dados relativos a sua plataforma de cobrança.
PlatformData platformData = new PlatformData(EPlatforms.PagHiper, "KEY", "TOKEN");

// Itens que comporão a cobrança.
ICollection<TransactionsItem> items = new List<TransactionsItem>();
items.Add(new TransactionsItem("Item 1", 149.90m));
items.Add(new TransactionsItem("Item 2", 10.10m));

// Itens que comporão os valores a serem descontados no total.
ICollection<TransactionsItem> discounts = new List<TransactionsItem>();
discounts.Add(new TransactionsItem("Desconto 1", 2.45m));
discounts.Add(new TransactionsItem("Desconto 2", 1.00m));

// Endereço do pagador.
Address address = new Address("Av Brigadeiro Faria Lima", 12345, "Jardim Paulistano", "São Paulo", "SP", "01452002", "Torre Sul 4º Andar");

// Telefone do pagador.
Telephone telephone = new Telephone(11, 40638785);

// Documento do pagador.
Document document = new Document(EDocumentType.CPF, "30307214001");

// Dados do pagador
Person payer = new Person("Poul Silva", "poulsilva@exemple.com", document, telephone, address);

// Dados da transação
Transaction transaction = new Transaction(platformData, transactionType, orderId, payer, dueDate, items, discounts);

```

Após a instancia dos objetos, prossiga com a execução da transação. No exemplo abaixo, está a Paghiper, mas o princípio é o mesmo para todas as demais.

```
// Crie uma transação básica.
Transaction transaction = Arrange.ForTransaction(transactionType);

// Instancie a plataforma de pagamento desejada.
PaghiperPlatform platform = new PaghiperPlatform();

// Recupere a transação com os dados atualizados.
transaction = await platform.Create(transaction);
```