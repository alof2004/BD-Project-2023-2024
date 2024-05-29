
use p8g3;

drop index IF EXISTS searchEmpresaNome on AgroTrack_Empresa;
drop index IF EXISTS searchEmpresa_Id_Empresa on AgroTrack_Empresa;
drop index IF EXISTS searchCliente on AgroTrack_Cliente;
drop index IF EXISTS searchAgricultorId on AgroTrack_Agricultor;
drop index IF EXISTS searchAgricultorPessoa_N_CartaoCidadao on AgroTrack_Agricultor;
drop index IF EXISTS searchAgricultorQuinta_Empresa_Id_Empresa on AgroTrack_Agricultor;
drop index IF EXISTS searchProdutoNome on AgroTrack_Produto;
drop index IF EXISTS searchProdutoId_Origem on AgroTrack_Produto;
drop index IF EXISTS searchProdutoPreco on AgroTrack_Produto;
drop index IF EXISTS searchProdutoTipo_De_Produto on AgroTrack_Produto;
drop index IF EXISTS seacrhContractStartDate on AgroTrack_Contrato;
drop index IF EXISTS searchContractEndDate on AgroTrack_Contrato;
drop index IF EXISTS searchContractSalary on AgroTrack_Contrato;
drop index IF EXISTS searchEncomendaCodigo on AgroTrack_Encomenda;
drop index IF EXISTS searchEncomendaData on AgroTrack_Encomenda;

-- Criacao de indexes na tabela AgroTrack_Empresa
create index searchEmpresa_Id_Empresa on AgroTrack_Empresa (Id_Empresa);
create index searchEmpresaNome on AgroTrack_Empresa (Nome);
-- Criacao de indexes na tabela AgroTrack_Cliente
create index searchCliente on AgroTrack_Cliente (Pessoa_N_CartaoCidadao);

-- Criacao de indexes na tabela AgroTrack_Agricultor
create index searchAgricultorId on AgroTrack_Agricultor (Id_Trabalhador);
create index searchAgricultorPessoa_N_CartaoCidadao on  AgroTrack_Agricultor (Pessoa_N_CartaoCidadao);
create index searchAgricultorQuinta_Empresa_Id_Empresa on  AgroTrack_Agricultor (Quinta_Empresa_Id_Empresa);

-- Criacao de indexes na tabela AgroTrack_Produto
create index searchProdutoNome on AgroTrack_Produto(Nome);
create index searchProdutoId_Origem on AgroTrack_Produto (Id_origem);
create index searchProdutoPreco on AgroTrack_Produto (Preco);
create index searchProdutoTipo_De_Produto on AgroTrack_Produto (Tipo_de_Produto);

-- Criacao de indexes na tabela AgroTrack_Contrato
create index seacrhContractStartDate on AgroTrack_Contrato ([Date_str]);
create index searchContractEndDate on AgroTrack_Contrato ([Date_end]);
create index searchContractSalary on AgroTrack_Contrato (Salario);

-- Criacao de indexes na tabela AgroTrack_Encomenda
create index searchEncomendaCodigo on AgroTrack_Encomenda (Codigo);
create index searchEncomendaData on AgroTrack_Encomenda (Entrega);




