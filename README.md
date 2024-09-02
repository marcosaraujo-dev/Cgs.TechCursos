# Cgs.TechCursos

![C#](https://img.shields.io/badge/language-C%23-blue) 
![xUnit](https://img.shields.io/badge/testing-xUnit-lightgrey)

**Cgs.TechCursos** é um projeto de simulação para o cadastro de uma escola de cursos. Ele permite o gerenciamento de alunos, cursos, professores e inscrições. O principal objetivo deste projeto é proporcionar uma base prática para o aprendizado de testes unitários e refatoração de código aplicando boas práticas de desenvolvimento de software.

## Objetivos do Projeto

- **Fase 01:** Implementar testes unitários utilizando o xUnit.
- **Fase 02:** Refatorar o código para aplicar boas práticas de desenvolvimento.
- **Fase 03:** IMplementação persistência com banco de dados

## Estrutura do Projeto

O projeto é dividido em três camadas principais: **Domain**, **Application** e **Infrastructure**, cada uma com sua própria responsabilidade:

.Cgs.TechCursos

├── **Cgs.TechCursos.Domain**

│   ├── Entities         # Contém as entidades do domínio, como Professor, Aluno, Curso, etc.

│   ├── Interfaces       # Define as interfaces dos repositórios e outros contratos do domínio.

│   ├── Notifications    # Implementa a lógica de notificações para validação e erros.

│   ├── Validators       # Contém classes de validação para as entidades do domínio.

├── **Cgs.TechCursos.Application**

│   ├── Services         # Contém os serviços que encapsulam a lógica de negócios e utilizam os repositórios.

├── **Cgs.TechCursos.Infrastructure**

│   ├── Repositories     # Implementa os repositórios que fazem o acesso a dados.

### Camadas do Projeto

- **Domain**: Esta camada contém as regras de negócio fundamentais. Aqui é onde ficam as entidades do domínio, interfaces de repositório, notificações para erros e validações.
- **Application**: Esta camada contém os serviços que coordenam a lógica de negócios. Eles utilizam os repositórios definidos na camada de `Domain` para manipular os dados.
- **Infrastructure**: Esta camada contém as implementações reais dos repositórios, incluindo o acesso a dados e cache. Ela faz a ponte entre a aplicação e os recursos de infraestrutura.

## Tecnologias Utilizadas

- **C#**: Linguagem de programação utilizada no projeto.
- **.NET Core**: Framework utilizado para a construção da aplicação.
- **xUnit**: Biblioteca de testes unitários utilizada para a criação de testes automatizados.
- **Moq**: Biblioteca utilizada para criação de objetos Mock em testes unitários.

## Configuração e Execução

Para rodar o projeto localmente, siga as etapas abaixo:

1. Clone o repositório:
```bash
   git clone https://github.com/marcosaraujo-dev/Cgs.TechCursos.git
   cd Cgs.TechCursos
```
2. Abra a solução no Visual Studio.

3. Restaure os pacotes NuGet:

```bash
dotnet restore
```

4. Compile o projeto:
```bash
dotnet build
```

5. Execute os Testes unitários
```bash
dotnet test
```

