DELETE FROM AgroTrack_Colhe;
DELETE FROM AgroTrack_Contem;
DELETE FROM AgroTrack_Empresa_De_Transportes;
DELETE FROM AgroTrack_Retalhistas;
DELETE FROM AgroTrack_Contrato;
DELETE FROM AgroTrack_Produto;
DELETE FROM AgroTrack_Agricultor;
DELETE FROM AgroTrack_Cliente;
DELETE FROM AgroTrack_Quinta_Planta;
DELETE FROM AgroTrack_Planta;
DELETE FROM AgroTrack_Quinta_Animal;
DELETE FROM AgroTrack_Animal;
DELETE FROM AgroTrack_Quinta;
DELETE FROM AgroTrack_Empresa;
DELETE FROM AgroTrack_Pessoa;

INSERT INTO AgroTrack_Pessoa(Nome,N_CartaoCidadao,Contacto) VALUES
('Joao Silva', 12345678, 912345678),
('Jorge Miguel', 87654321, 912345678),
('Maria Sousa', 98765432, 934567890),
('António Ferreira', 45678912, 923456789),
('Afonso Lucas', 18041804, 918049638),
('Beatriz Oliveira', 75395128, 925874136),
('Pedro Santos', 36925814, 917253684),
('Sofia Costa', 24681357, 926487135),
('Miguel Rodrigues', 58246913, 913824769),
('Carolina Pereira', 31472658, 919372645),
('Rui Martins', 95135786, 928465713),
('Inês Fernandes', 82461357, 927346185),
('Luís Almeida', 75315986, 915278463),
('Ana Sousa', 36985214, 931475869),
('Tiago Oliveira', 74125896, 932587146),
('Francisco Pereira', 36914725, 938571624),
('Catarina Santos', 15948762, 935824716),
('Diana Costa', 78523691, 934185276),
('Gonçalo Rodrigues', 25836947, 917524836),
('Rita Martins', 36985247, 936487251),
('Eduardo Oliveira', 78945612, 910203040),
('Isabel Santos', 65498732, 915171819),
('Leonardo Costa', 32165498, 920304050),
('Marta Ferreira', 98732165, 925262728),
('Ricardo Pereira', 45612378, 930384950),
('Vera Santos', 58214736, 916243785),
('Andreia Costa', 15948763, 935824716),
('Xavier Martins', 36985249, 936487253),
('Yvone Oliveira', 78945614, 910203042),
('Zé Santos', 65498734, 915171821),
('Álvaro Costa', 32165400, 920304052),
('Bárbara Ferreira', 98732167, 925262730),
('Carlos Pereira', 45612380, 930384952),
('Diana Carvalho', 36985217, 931475873),
('Eduardo Rodrigues', 74125903, 932587150),
('Fátima Santos', 58214740, 916243788),
('Gilberto Oliveira', 36914728, 938571628),
('Helena Costa', 15948766, 935824720);

INSERT INTO AgroTrack_Empresa(Id_Empresa, Nome, Morada) VALUES
(1, 'Quinta da Vinha Verde', 'Rua Principal, 123'),
(2, 'Quinta da Esperança', 'Avenida Central, 456'),
(3, 'Quinta da Boa Vista', 'Travessa Secundária, 789'),
(4, 'Quinta da Fonte Fresca', 'Rua das Flores, 10'),
(5, 'Quinta das Oliveiras', 'Avenida dos Aliados, 20'),
(6, 'Logística Ágil', 'Avenida das Entregas Eficientes, 60'),
(7, 'Fretes Seguros', 'Travessa dos Transportes Seguros, 70'),
(8, 'Transportes Velozes LTDA', 'Rua das Rotas R�pidas, 80'),
(9, 'Logistica Global', 'Avenida da Efici�ncia Log�stica, 90'),
(10, 'Entrega Segura', 'Travessa da Entrega Certa, 100'),
(11, 'Supermercado Central', 'Rua Principal, 250'),
(12, 'Loja do Bairro', 'Avenida Central, 260'),
(13, 'Casa dos Utensilios', 'Travessa das Utilidades, 270'),
(14, 'Continente', 'Rua das Novidades, 280'),
(15, 'Pingo Doce', 'Avenida das Mercadorias, 290');

INSERT INTO AgroTrack_Quinta(Codigo_quinta, Empresa_Id_Empresa) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5)

INSERT INTO AgroTrack_Animal(Id_Animal, Tipo_de_Animal) VALUES
(1, 'Vaca'),
(2, 'Ovelha'),
(3, 'Porco'),
(4, 'Frango'),
(5, 'Cavalo'),
(6, 'Cabra'),
(7, 'Pato'),
(8, 'Ganso'),
(9, 'Gato'),
(10, 'Coelho'),
(11, 'Burro'),
(12, 'Galinha'),
(13, 'Peru'),
(14, 'Cão');

INSERT INTO AgroTrack_Quinta_Animal(Empresa_Id_Empresa, Idade, Brinco, Id_Animal) VALUES
(1, 5, '1VAC', 1),
(1, 8, '2VAC', 1),
(1, 4, '1BUR', 11),
(1, 6, '2BUR', 11),
(2, 1, '1GLN', 12),
(2, 2, '2GLN', 12),
(2, 1, '1CAO', 14),
(2, 2, '2CAO', 14),
(2, 3, '1GAN', 8),
(3, 3, '1PRU', 13),
(3, 4, '2PRU', 13),
(2, 5, '2GAN', 8),
(3, 2, '3POR', 3),
(3, 3, '4POR', 3),
(4, 1, '1OVL', 2),
(4, 2, '2OVL', 2),
(5, 4, '1CAV', 5),
(5, 6, '2CAV', 5),
(1, 2, '1CAB', 6),
(1, 3, '2CAB', 6),
(2, 1, '1PAT', 7),
(2, 2, '2PAT', 7),
(5, 1, '1GAN', 8),
(5, 2, '2GAN', 8);


INSERT INTO AgroTrack_Planta(Id_planta, Tipo, Estacao) VALUES
(51,'Macieira', 'Primavera'),
(52,'Alface', 'Verão'),
(53,'Macieira', 'Verão'),
(54,'Tomate', 'Outono'),
(55,'Laranjeira', 'Todas'),
(56, 'Pereira', 'Primavera'),
(57,'Alface', 'Primavera'),
(58,'Tomate', 'Primavera'),
(59,'Milho', 'Verão'),
(60,'Cerejeira', 'Verão'),
(61,'Melão', 'Verão'),
(62,'Bananeira', 'Todas'),
(63, 'Cenoura', 'Inverno'),
(64, 'Abóbora', 'Outono'),
(65, 'Morangueiro', 'Primavera'),
(66, 'Videira', 'Verão'),
(67, 'Feijão', 'Verão'),
(68, 'Cebola', 'Inverno'),
(69, 'Batata', 'Inverno');

INSERT INTO AgroTrack_Quinta_Planta(Empresa_Id_Empresa, Lote, Id_Planta) VALUES
(1, 'A1', 51),
(1, 'A2', 52),
(1, 'A3', 53),
(1, 'A4', 54),
(1, 'A5', 55),
(2, 'B1', 56),
(2, 'B2', 57),
(2, 'B3', 58),
(2, 'B4', 59),
(2, 'B5', 60),
(3, 'C1', 61),
(3, 'C2', 62),
(3, 'C3', 63),
(3, 'C4', 64),
(3, 'C5', 65),
(4, 'D1', 66),
(4, 'D2', 67),
(4, 'D3', 68),
(4, 'D4', 69),
(4, 'D5', 51),
(5, 'E1', 52),
(5, 'E2', 53),
(5, 'E3', 54),
(5, 'E4', 55),
(5, 'E5', 56);


INSERT INTO AgroTrack_Cliente(Pessoa_N_CartaoCidadao) VALUES
(36985249),
(32165498),
(36985217),
(65498732),
(98732165);




INSERT INTO AgroTrack_Agricultor(Id_Trabalhador, Pessoa_N_CartaoCidadao,Quinta_Empresa_Id_Empresa) VALUES
(1, 12345678, 1),
(2, 87654321, 1),
(1, 18041804, 2),
(2, 15948762, 2),
(1, 36985247, 3),
(2, 95135786, 3),
(1, 74125896, 4),
(2, 36914725, 4),
(1, 58214736, 5),
(2, 36985214, 5);

INSERT INTO AgroTrack_Produto(Nome, Id_origem, Tipo_de_Produto, Codigo, Preco, Taxa_de_iva, Unidade_medida) VALUES
('Maçã', 1, 1, 1, 1.5, 0.23, 'kg'),
('Laranja', 5, 1, 2, 1.2, 0.23, 'kg'),
('Alface', 2, 2, 3, 0.5, 0.13, 'unidade'),
('Pêra', 6, 1, 4, 1.3, 0.23, 'kg'),
('Tomate', 8, 2, 5, 0.8, 0.13, 'kg'),
('Milho', 9, 2, 6, 0.6, 0.13, 'kg'),
('Cereja', 10, 1, 7, 2.0, 0.23, 'kg'),
('Melão', 11, 1, 8, 1.0, 0.13, 'unidade'),
('Banana', 12, 1, 9, 1.5, 0.23, 'kg'),
('Cenoura', 13, 2, 10, 0.4, 0.13, 'kg'),
('Abóbora', 14, 2, 11, 0.7, 0.13, 'kg'),
('Morango', 15, 1, 12, 2.5, 0.23, 'kg'),
('Vinho', 16, 1, 13, 3.0, 0.23, 'litro'),
('Feijão', 17, 2, 14, 0.8, 0.13, 'kg'),
('Cebola', 18, 2, 15, 0.6, 0.13, 'kg'),
('Batata', 19, 2, 16, 0.5, 0.13, 'kg'),
('Pato', 20, 3, 17, 5.0, 0.23, 'kg'),
('Ganso', 21, 3, 18, 4.0, 0.23, 'kg'),
('Galinha', 22, 3, 19, 3.0, 0.23, 'kg'),
('Peru', 23, 3, 20, 6.0, 0.23, 'kg'),
('Vaca', 24, 3, 21, 7.0, 0.23, 'kg'),
('Ovelha', 25, 3, 22, 8.0, 0.23, 'kg'),
('Porco', 26, 3, 23, 9.0, 0.23, 'kg'),
('Frango', 27, 3, 24, 3.0, 0.23, 'kg'),
('Leite', 28, 3, 25, 10.0, 0.23, 'litro'),
('Cabra', 29, 3, 26, 8.0, 0.23, 'kg'),
('Coelho', 31, 3, 28, 2.0, 0.23, 'kg');


INSERT INTO AgroTrack_Contrato(Date_str, Date_end, Descricao, Salario, ID, Agricultor_Pessoa_N_CartaoCidadao) VALUES
('2024-01-01', '2024-12-31', 'Contrato de trabalho', 1000, 1, 12345678),
('2024-01-01', '2024-12-31', 'Contrato de trabalho1', 1000, 2, 87654321),
('2024-01-01', '2025-12-31', 'Contrato de trabalho2', 1000, 3, 18041804),
('2024-01-01', '2025-12-31', 'Contrato de trabalho3', 1000, 4, 15948762),
('2024-01-01', '2026-12-31', 'Contrato de trabalho4', 1000, 5, 36985247),
('2024-01-01', '2026-12-31', 'Contrato de trabalho5', 1000, 6, 95135786),
('2024-01-01', '2026-12-31', 'Contrato de trabalho6', 1000, 7, 74125896),
('2024-01-01', '2026-12-31', 'Contrato de trabalho7', 1000, 8, 36914725),
('2024-01-01', '2026-12-31', 'Contrato de trabalho8', 1000, 9, 58214736),
('2024-01-01', '2026-12-31', 'Contrato de trabalh9', 1000, 10, 36985214);

INSERT INTO AgroTrack_Retalhistas VALUES
	(11),
	(12),
	(13),
	(14),
	(15);

INSERT INTO AgroTrack_Empresa_De_Transportes (Empresa_Id_Empresa) VALUES
	(6),
	(7),
	(8),
	(9),
	(10);

INSERT INTO AgroTrack_Contem (Produto_codigo, Quinta_Empresa_Id_Empresa, Data_de_validade, Quantidade) VALUES
	(1, 1, '2024-07-01', 100),
	(2, 1, '2024-07-01', 100),
	(3, 1, '2024-07-01', 100),
	(4, 1, '2024-06-01', 100),
	(5, 1, '2024-07-01', 100),
	(6, 1, '2024-06-01', 100),
	(7, 1, '2024-05-01', 100),
	(8, 1, '2024-07-01', 100),
	(9, 1, '2024-06-01', 100),
	(10, 1, '2024-06-12', 100),
	(11, 1, '2024-06-13', 100),
	(12, 1, '2024-07-12', 100),
	(13, 1, '2024-06-14', 100),
	(14, 1, '2024-06-12', 100),
	(15, 1, '2024-06-21', 100),
	(16, 1, '2024-05-30', 100);

INSERT INTO AgroTrack_Colhe(Agricultor_Pessoa_N_CartaoCidadao, Duracao_colheita,Quantidade,Produto_codigo,DataColheita) VALUES
	(12345678, 2.5, 100, 1, '2024-05-01'),
	(87654321, 2.5, 100, 2, '2024-05-21'),
	(18041804, 2.5, 100, 3, '2024-05-25'),
	(15948762, 2.5, 100, 4, '2024-05-26'),
	(36985247, 2.5, 100, 5, '2024-05-27'),
	(95135786, 2.5, 100, 6, '2024-05-30'),
	(74125896, 2.5, 100, 7, '2024-05-13'),
	(36914725, 2.5, 100, 8, '2024-05-15'),
	(58214736, 2.5, 100, 9, '2024-05-01'),
	(36985214, 2.5, 100, 10, '2024-05-07'),
	(12345678, 2.5, 100, 11, '2024-05-06'),
	(87654321, 2.5, 100, 12, '2024-06-05'),
	(18041804, 2.5, 100, 13, '2024-06-04'),
	(15948762, 2.5, 100, 14, '2024-06-01'),
	(36985247, 2.5, 100, 15, '2024-06-01'),
	(95135786, 2.5, 100, 16, '2024-06-01');

INSERT INTO AgroTrack_Encomenda(Codigo, prazo_entrega, Morada_entrega, Entrega, Retalhista_Empresa_Id_Empresa, Empresa_De_Transportes_Id_Empresa, Quinta_Empresa_Id) VALUES
	(1, 2, 'Rua Principal, 250', '2024-06-01', 11, 6, 1),
	(2, 3, 'Avenida Central, 260', '2024-06-02', 12, 7, 2),
	(3, 4, 'Travessa das Utilidades, 270', '2024-06-03', 13, 8, 3),
	(4, 5, 'Rua das Novidades, 280', '2024-06-04', 14, 9, 4),
	(5, 6, 'Avenida das Mercadorias, 290', '2024-06-05', 15, 10, 5),
	

INSERT INTO AgroTrack_Item(ProdutoCodigo, Quantidade, Encomenda_Codigo) VALUES
	(1, 100, 1),
	(2, 100, 2),
	(3, 100, 3),
	(4, 100, 4),
	(5, 100, 5);
