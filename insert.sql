DELETE FROM AgroTrack_Compra;
DELETE FROM AgroTrack_Item;
DELETE FROM AgroTrack_Encomenda;
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
('Helena Costa', 15948766, 935824720),
('Igor Mendes', 14567892, 912367892),
('Juliana Almeida', 23456789, 913489276),
('Karen Barbosa', 34567891, 914567234),
('Lucas Martins', 45678934, 915678923),
('Marlene Sousa', 56789123, 916789342),
('Nelson Ribeiro', 67891234, 917893421),
('Olivia Lopes', 78912345, 918934567),
('Paulo Nogueira', 89123456, 919045678),
('Quintino Pinto', 91234567, 920156789),
('Rosa Fonseca', 10234567, 921267890),
('Sergio Neves', 20345678, 922378901),
('Tânia Vieira', 30456789, 923489012),
('Ulisses Cunha', 40567890, 924590123),
('Vítor Matos', 50678901, 925601234),
('Wagner Lima', 60789012, 926712345),
('Ximena Teixeira', 70890123, 927823456),
('Yasmin Duarte', 80901234, 928934567),
('Zélia Rocha', 90123456, 930045678);


INSERT INTO AgroTrack_Empresa(Id_Empresa, Nome, Morada, Contacto, Tipo_De_Empresa) VALUES
(1, 'Quinta da Vinha Verde', 'Rua Principal, 123', 919000923, 'Quinta'),
(2, 'Logística Ágil', 'Avenida das Entregas Eficientes, 60', 919350928, 'Transportadora'),
(3, 'Supermercado Central', 'Rua Principal, 250', 919946933, 'Retalhista'),
(4, 'Quinta da Esperança', 'Avenida Central, 456', 919000924, 'Quinta'),
(5, 'Fretes Seguros', 'Travessa dos Transportes Seguros, 70', 919000929, 'Transportadora'),
(6, 'Loja do Bairro', 'Avenida Central, 260', 919358934, 'Retalhista'),
(7, 'Quinta da Boa Vista', 'Travessa Secundária, 789', 919057925, 'Quinta'),
(8, 'Transportes Velozes LTDA', 'Rua das Rotas Rápidas, 80', 919000930, 'Transportadora'),
(9, 'Casa dos Utensílios', 'Travessa das Utilidades, 270', 919432935, 'Retalhista'),
(10, 'Quinta da Fonte Fresca', 'Rua das Flores, 10', 919340926, 'Quinta'),
(11, 'Logística Global', 'Avenida da Eficiência Logística, 90', 919000931, 'Transportadora'),
(12, 'Continente', 'Rua das Novidades, 280', 919673936, 'Retalhista'),
(13, 'Quinta das Oliveiras', 'Avenida dos Aliados, 20', 919033257, 'Quinta'),
(14, 'Entrega Segura', 'Travessa da Entrega Certa, 100', 919000932, 'Transportadora'),
(15, 'Pingo Doce', 'Avenida das Mercadorias, 290', 919000937, 'Retalhista'),
(16, 'Quinta do Sol', 'Rua da Luz, 300', 919012938, 'Quinta'),
(17, 'Transporte Rápido', 'Rua das Entregas, 350', 919067943, 'Transportadora'),
(18, 'Mercado Local', 'Rua do Comércio, 400', 919122948, 'Retalhista'),
(19, 'Quinta do Vale', 'Avenida das Colinas, 310', 919023939, 'Quinta'),
(20, 'Entrega Confiável', 'Avenida da Segurança, 360', 919078944, 'Transportadora'),
(21, 'Loja de Conveniência', 'Avenida dos Produtos, 410', 919133949, 'Retalhista'),
(22, 'Quinta da Alegria', 'Travessa da Felicidade, 320', 919034940, 'Quinta'),
(23, 'Logística Precisão', 'Travessa da Pontualidade, 370', 919089945, 'Transportadora'),
(24, 'Supermercado do Bairro', 'Travessa do Supermercado, 420', 919144950, 'Retalhista'),
(25, 'Quinta do Horizonte', 'Rua do Pôr do Sol, 330', 919045941, 'Quinta'),
(26, 'Transporte Certeiro', 'Rua da Precisão, 380', 919100946, 'Transportadora'),
(27, 'Mercearia da Esquina', 'Rua das Compras, 430', 919155951, 'Retalhista'),
(28, 'Quinta do Mar', 'Avenida da Praia, 340', 919056942, 'Quinta'),
(29, 'Entrega Eficiente', 'Avenida da Rapidez, 390', 919111947, 'Transportadora'),
(30, 'Hipermercado Total', 'Avenida dos Grandes Produtos, 440', 919166952, 'Retalhista');



INSERT INTO AgroTrack_Quinta(Empresa_Id_Empresa) VALUES
(1),
(4),
(7),
(10),
(13),
(16),
(19),
(22),
(25),
(28);

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
(14, 'Cão'),
(15, 'Bode'),
(16, 'Pavão'),
(17, 'Pombo'),
(18, 'Codorna'),
(19, 'Pintinho'),
(20, 'Carneiro'),
(21, 'Lhama'),
(22, 'Alpaca'),
(23, 'Javali'),
(24, 'Mulherengo'),
(25, 'Galo'),
(26, 'Búfalo'),
(27, 'Pônei'),
(28, 'Camelo'),
(29, 'Dromedário'),
(30, 'Iaque');


INSERT INTO AgroTrack_Quinta_Animal(Empresa_Id_Empresa, Idade, Brinco, Id_Animal) VALUES
(1, 5, '1VAC', 1),
(1, 8, '2VAC', 1),
(1, 4, '1BUR', 11),
(1, 6, '2BUR', 11),
(4, 1, '1GLN', 12),
(4, 2, '2GLN', 12),
(4, 1, '1CAO', 14),
(4, 2, '2CAO', 14),
(4, 3, '1GAN', 8),
(7, 3, '1PRU', 13),
(7, 4, '2PRU', 13),
(7, 5, '2GAN', 8),
(10, 2, '3POR', 3),
(10, 3, '4POR', 3),
(16, 1, '1OVL', 2),
(16, 2, '2OVL', 2),
(19, 4, '1CAV', 5),
(19, 6, '2CAV', 5),
(19, 2, '1CAB', 6),
(22, 3, '2CAB', 6),
(22, 1, '1PAT', 7),
(22, 2, '2PAT', 7),
(25, 1, '1GAN', 8),
(28, 2, '2GAN', 8);


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
(4, 'B1', 56),
(4, 'B2', 57),
(7, 'B3', 58),
(7, 'B4', 59),
(7, 'B5', 60),
(7, 'C1', 61),
(10, 'C2', 62),
(10, 'C3', 63),
(10, 'C4', 64),
(13, 'C5', 65),
(13, 'D1', 66),
(13, 'D2', 67),
(16, 'D3', 68),
(16, 'D4', 69),
(19, 'D5', 51),
(19, 'E1', 52),
(22, 'E2', 53),
(22, 'E3', 54),
(25, 'E4', 55),
(28, 'E5', 56);


INSERT INTO AgroTrack_Cliente(Pessoa_N_CartaoCidadao) VALUES
(36985249),
(32165498),
(36985217),
(65498732),
(98732165),
(18041804),
(36925814),
(24681357),
(58246913),
(31472658),
(95135786),
(82461357),
(75315986),
(36985214),
(74125896),
(98765432),
(45678912),
(75395128);


INSERT INTO AgroTrack_Agricultor(Id_Trabalhador, Pessoa_N_CartaoCidadao, Quinta_Empresa_Id_Empresa) VALUES
(3, 98765432, 1),
(4, 45678912, 1),
(3, 36925814, 7),
(4, 24681357, 7),
(3, 58246913, 4),
(4, 31472658, 10),
(3, 95135786, 10),
(4, 82461357, 13),
(3, 75315986, 10),
(4, 36985214, 10),
(5, 12345678, 1),
(6, 23456789, 1),
(7, 34567890, 4),
(8, 45678901, 4),
(9, 56789012, 7),
(10, 67890123, 7),
(11, 78901234, 10),
(12, 89012345, 10),
(13, 90123456, 13),
(25, 11223344, 16),
(26, 22334455, 16),
(27, 33445566, 19),
(28, 44556677, 19),
(29, 55667788, 22),
(30, 66778899, 22),
(31, 77889900, 25),
(32, 88990011, 25),
(33, 99001122, 28),
(34, 00112233, 28);

INSERT INTO AgroTrack_Produto(Nome, Tipo_de_Produto, Codigo, Preco, Taxa_de_iva, Unidade_medida) VALUES
('Maçã', '1', 1, 1.5, 0.23, 'kg'),
('Laranja','1', 2, 1.2, 0.23, 'kg'),
('Alface', '2', 3, 0.5, 0.13, 'unidade'),
('Pêra', '1', 4, 1.3, 0.23, 'kg'),
('Tomate', '2', 5, 0.8, 0.13, 'kg'),
('Milho', '2', 6, 0.6, 0.13, 'kg'),
('Cereja', '1', 7, 2.0, 0.23, 'kg'),
('Melão', '1', 8, 1.0, 0.13, 'unidade'),
('Banana', '1', 9, 1.5, 0.23, 'kg'),
('Cenoura', '2', 10, 0.4, 0.13, 'kg'),
('Abóbora', '2', 11, 0.7, 0.13, 'kg'),
('Morango', '1', 12, 2.5, 0.23, 'kg'),
('Vinho', '1', 13, 3.0, 0.23, 'litro'),
('Feijão',  '2', 14, 0.8, 0.13, 'kg'),
('Cebola', '2', 15, 0.6, 0.13, 'kg'),
('Batata', '2', 16, 0.5, 0.13, 'kg'),
('Pato',  '3', 17, 5.0, 0.23, 'kg'),
('Ganso',  '3', 18, 4.0, 0.23, 'kg'),
('Galinha',  '3', 19, 3.0, 0.23, 'kg'),
('Peru',  '3', 20, 6.0, 0.23, 'kg'),
('Vaca',  '3', 21, 7.0, 0.23, 'kg'),
('Ovelha',  '3', 22, 8.0, 0.23, 'kg'),
('Porco',  '3', 23, 9.0, 0.23, 'kg'),
('Frango',  '3', 24, 3.0, 0.23, 'kg'),
('Leite',  '3', 25, 10.0, 0.23, 'litro'),
('Cabra',  '3', 26, 8.0, 0.23, 'kg'),
('Coelho',  '3', 28, 2.0, 0.23, 'kg');



UPDATE AgroTrack_Produto
SET Tipo_de_Produto = 
    CASE 
        WHEN Tipo_de_Produto = '1' THEN 'Fruto'
        WHEN Tipo_de_Produto = '2' THEN 'Legume'
        WHEN Tipo_de_Produto = '3' THEN 'Animal'
        ELSE Tipo_de_Produto
    END;

INSERT INTO AgroTrack_Contrato(Date_str, Date_end, Descricao, Salario, ID, Agricultor_Pessoa_N_CartaoCidadao) VALUES
('2024-01-01', '2024-12-31', 'Contrato de trabalho', 1000, 1, 98765432),
('2024-01-01', '2024-12-31', 'Contrato de trabalho1', 1000, 2, 45678912),
('2024-01-01', '2024-12-31', 'Contrato de trabalho2', 1000, 3, 36925814),
('2024-01-01', '2024-12-31', 'Contrato de trabalho3', 1000, 4, 24681357),
('2024-01-01', '2024-12-31', 'Contrato de trabalho4', 1000, 5, 58246913),
('2024-01-01', '2024-12-31', 'Contrato de trabalho5', 1000, 6, 31472658),
('2024-01-01', '2024-12-31', 'Contrato de trabalho6', 1000, 7, 95135786),
('2024-01-01', '2024-12-31', 'Contrato de trabalho7', 1000, 8, 82461357),
('2024-01-01', '2024-12-31', 'Contrato de trabalho8', 1000, 9, 75315986),
('2024-01-01', '2024-12-31', 'Contrato de trabalho9', 1000, 10, 36985214),
('2024-01-01', '2024-12-31', 'Contrato de trabalho10', 1000, 11, 12345678),
('2024-01-01', '2024-12-31', 'Contrato de trabalho11', 1000, 12, 23456789),
('2024-01-01', '2024-12-31', 'Contrato de trabalho12', 1000, 13, 34567890),
('2024-01-01', '2024-12-31', 'Contrato de trabalho13', 1000, 14, 45678901),
('2024-01-01', '2024-12-31', 'Contrato de trabalho14', 1000, 15, 56789012),
('2024-01-01', '2024-12-31', 'Contrato de trabalho15', 1000, 16, 67890123),
('2024-01-01', '2024-12-31', 'Contrato de trabalho16', 1000, 17, 78901234),
('2024-01-01', '2024-12-31', 'Contrato de trabalho17', 1000, 18, 89012345),
('2024-01-01', '2024-12-31', 'Contrato de trabalho18', 1000, 19, 90123456),
('2024-01-01', '2024-12-31', 'Contrato de trabalho19', 1000, 20, 11223344),
('2024-01-01', '2024-12-31', 'Contrato de trabalho20', 1000, 21, 22334455),
('2024-01-01', '2024-12-31', 'Contrato de trabalho21', 1000, 22, 33445566),
('2024-01-01', '2024-12-31', 'Contrato de trabalho22', 1000, 23, 44556677),
('2024-01-01', '2024-12-31', 'Contrato de trabalho23', 1000, 24, 55667788),
('2024-01-01', '2024-12-31', 'Contrato de trabalho24', 1000, 25, 66778899),
('2024-01-01', '2024-12-31', 'Contrato de trabalho25', 1000, 26, 77889900),
('2024-01-01', '2024-12-31', 'Contrato de trabalho26', 1000, 27, 88990011),
('2024-01-01', '2024-12-31', 'Contrato de trabalho27', 1000, 28, 99001122),
('2024-01-01', '2024-12-31', 'Contrato de trabalho28', 1000, 29, 00112233);

INSERT INTO AgroTrack_Retalhistas VALUES
(11),
(12),
(13),
(14),
(15),
(16),
(17),
(18),
(19),
(20),
(21),
(22),
(23),
(24),
(25),
(26),
(27),
(28),
(29),
(30);

INSERT INTO AgroTrack_Empresa_De_Transportes (Empresa_Id_Empresa, Nome) VALUES
    (2, 'Transportadora A'),
    (5, 'Transportadora B'),
    (8, 'Transportadora C'),
    (11, 'Transportadora D'),
    (14, 'Transportadora E'),
    (17, 'Transportadora F'),
    (20, 'Transportadora G'),
    (23, 'Transportadora H'),
    (26, 'Transportadora I'),
    (29, 'Transportadora J');

INSERT INTO AgroTrack_Contem (Produto_codigo, Quinta_Empresa_Id_Empresa, Data_de_validade, Quantidade) VALUES
    (1, 4, '2024-07-01', 29),
    (3, 7, '2024-07-01', 32),
    (4, 13, '2024-06-01', 44),
    (5, 7, '2024-07-01', 95),
    (6, 7, '2024-06-01', 143),
    (7, 10, '2024-05-01', 123),
    (8, 10, '2024-07-01', 67),
    (9, 10, '2024-06-01', 55),
    (10, 10, '2024-06-12', 15),
    (11, 10, '2024-06-13', 100),
    (12, 13, '2024-07-12', 100),
    (13, 13, '2024-06-14', 100),
    (14, 13, '2024-06-12', 100),
    (15, 13, '2024-06-21', 100),
    (16, 13, '2024-05-30', 100),
    (2, 4, '2024-08-01', 120),
    (10, 7, '2024-09-01', 240),
    (5, 10, '2024-10-01', 210),
    (6, 13, '2024-11-01', 30),
    (7, 7, '2024-12-01', 50),
    (12, 10, '2024-01-01', 40),
    (10, 1, '2024-02-01', 350),
    (8, 4, '2024-03-01', 200),
    (5, 1, '2024-04-01', 450),
    (3, 1, '2024-05-01', 20),
    (1, 7, '2024-07-01', 10),
    (2, 10, '2024-07-01', 20),
    (3, 13, '2024-07-01', 30),
    (4, 16, '2024-07-01', 40),
    (5, 19, '2024-07-01', 50),
    (6, 22, '2024-07-01', 60),
    (7, 25, '2024-07-01', 70),
    (8, 28, '2024-07-01', 80);



INSERT INTO AgroTrack_Colhe (Agricultor_Pessoa_N_CartaoCidadao, Duracao_colheita, Quantidade, Produto_codigo, DataColheita) VALUES
    (98765432, 2.5, 100, 1, '2024-05-01'),
    (45678912, 2.5, 100, 2, '2024-05-21'),
    (36925814, 2.5, 100, 3, '2024-05-25'),
    (24681357, 2.5, 100, 4, '2024-05-26'),
    (58246913, 2.5, 100, 5, '2024-05-27'),
    (31472658, 2.5, 100, 6, '2024-05-30'),
    (95135786, 2.5, 100, 7, '2024-05-13'),
    (82461357, 2.5, 100, 8, '2024-05-15'),
    (75315986, 2.5, 100, 9, '2024-05-01'),
    (36985214, 2.5, 100, 10, '2024-05-07'),
    (12345678, 2.5, 100, 11, '2024-05-06'),
    (23456789, 2.5, 100, 12, '2024-06-05'),
    (34567890, 2.5, 100, 13, '2024-06-04'),
    (45678901, 2.5, 100, 14, '2024-06-01'),
    (56789012, 2.5, 100, 15, '2024-06-01'),
    (67890123, 2.5, 100, 16, '2024-06-01');

INSERT INTO AgroTrack_Encomenda (Codigo, prazo_entrega, Morada_entrega, Entrega, Retalhista_Empresa_Id_Empresa, Empresa_De_Transportes_Id_Empresa, Quinta_Empresa_Id) VALUES
    (1, 2, 'Rua Principal, 250', '2024-05-01', 11, 2, 1),
    (2, 3, 'Avenida Central, 260', '2024-05-02', 12, 5, 4),
    (3, 4, 'Travessa das Utilidades, 270', '2024-06-03', 13, 5, 7),
    (4, 5, 'Rua das Novidades, 280', '2024-07-03', 14, 8, 4),
    (5, 6, 'Avenida das Mercadorias, 290', '2024-09-12', 15, 8, 7),
    (6, 7, 'Praça das Flores, 300', '2024-05-01', 11, 11,10),
    (7, 8, 'Largo do Mercado, 310', '2024-05-02', 12, 14, 10),
    (8, 9, 'Beco do Comércio, 320', '2024-06-03', 13, 14, 10),
    (9, 10, 'Estrada Velha, 330', '2024-07-03', 14, 14, 13),
    (10, 11, 'Alameda das Árvores, 340', '2024-09-12', 15, 14, 13),
    (11, 12, 'Rua do Sol, 350', '2024-05-01', 11, 17, 13),
    (12, 13, 'Avenida das Nações, 360', '2024-05-02', 12, 20, 16),
    (13, 14, 'Travessa da Esperança, 370', '2024-06-03', 13, 23, 16),
    (14, 15, 'Rua das Flores, 380', '2024-07-03', 14, 23, 16),
    (15, 16, 'Avenida do Futuro, 390', '2024-09-12', 15, 23, 16),
    (16, 17, 'Rua da Liberdade, 400', '2024-05-01', 11, 26, 19),
    (17, 18, 'Avenida dos Sonhos, 410', '2024-05-02', 12, 26, 19),
    (18, 19, 'Praça das Alegrias, 420', '2024-06-03', 13, 26, 19),
    (19, 20, 'Beco das Maravilhas, 430', '2024-07-03', 14, 26, 19),
    (20, 21, 'Largo das Estrelas, 440', '2024-09-12', 15, 29, 22),
    (21, 22, 'Rua da Paz, 450', '2024-05-01', 11, 29, 22),
    (22, 23, 'Avenida da Harmonia, 460', '2024-05-02', 12, 29, 22),
    (23, 24, 'Travessa do Amor, 470', '2024-06-03', 13, 29, 22),
    (24, 25, 'Rua dos Desejos, 480', '2024-07-03', 14, 29, 25),
    (25, 26, 'Avenida da Felicidade, 490', '2024-09-12', 15, 11, 25),
    (26, 27, 'Praça da Sabedoria, 500', '2024-05-01', 11, 11, 25),
    (27, 28, 'Largo do Conhecimento, 510', '2024-05-02', 12, 11, 28),
    (28, 29, 'Rua da Intuição, 520', '2024-06-03', 13, 2, 28),
    (29, 30, 'Avenida da Inspiração, 530', '2024-07-03', 14, 2, 28),
    (30, 31, 'Travessa da Imaginação, 540', '2024-09-12', 15, 5, 28);


-- Adicionando itens com quantidades diferentes a cada encomenda
INSERT INTO AgroTrack_Item (ProdutoCodigo, Quantidade, Encomenda_Codigo) VALUES
    (6, 45, 1),
    (7, 60, 1),
    (8, 55, 2),
    (9, 40, 2),
    (10, 70, 3),
    (11, 30, 3),
    (12, 65, 4),
    (13, 35, 4),
    (14, 80, 5),
    (15, 25, 5),
    (16, 48, 6),
    (17, 52, 6),
    (18, 53, 7),
    (19, 47, 7),
    (20, 75, 8),
    (21, 25, 8),
    (22, 60, 9),
    (23, 40, 9),
    (24, 72, 10),
    (25, 28, 10),
    (26, 55, 11),
    (27, 45, 11),
    (28, 50, 12),
    (29, 50, 12),
    (30, 65, 13),
    (31, 35, 13),
    (32, 45, 14),
    (33, 55, 14),
    (34, 88, 15),
    (35, 12, 15),
    (36, 62, 16),
    (37, 38, 16),
    (38, 75, 17),
    (39, 25, 17),
    (40, 90, 18),
    (41, 10, 18),
    (42, 45, 19),
    (43, 55, 19),
    (44, 68, 20),
    (45, 32, 20),
    (46, 47, 21),
    (47, 53, 21),
    (48, 58, 22),
    (49, 42, 22),
    (50, 77, 23),
    (51, 23, 23),
    (52, 48, 24),
    (53, 52, 24),
    (54, 92, 25),
    (55, 8, 25),
    (56, 60, 26),
    (57, 40, 26),
    (58, 65, 27),
    (59, 35, 27),
    (60, 50, 28),
    (61, 50, 28),
    (62, 70, 29),
    (63, 30, 29),
    (64, 78, 30),
    (65, 22, 30);


INSERT INTO AgroTrack_Compra(Produto_codigo, Cliente_Pessoa_N_CartaoCidadao, Preco, Quantidade, Metodo_de_pagamento, ID_Quinta, DataCompra) VALUES
    (1, 36985249, 18, 12, 'MBWay', 1, '2024-05-01'),
    (2, 32165498, 12, 10, 'Cartão de Crédito', 2, '2024-05-21'),
    (3, 36985217, 10, 20, 'Dinheiro', 3, '2024-05-25'),
    (4, 65498732, 10.4, 8, 'Cartão de Débito', 4, '2024-05-26'),
    (5, 98732165, 4, 5, 'MBWay', 5, '2024-05-27'),
    (6, 18041804, 15, 15, 'Cartão de Crédito', 1, '2024-05-28'),
    (7, 36925814, 8.5, 10, 'Dinheiro', 2, '2024-05-29'),
    (8, 24681357, 20, 6, 'MBWay', 3, '2024-05-30'),
    (9, 58246913, 6.7, 9, 'Cartão de Débito', 4, '2024-06-01'),
    (10, 31472658, 12, 7, 'MBWay', 5, '2024-06-02'),
    (11, 95135786, 18, 15, 'Cartão de Crédito', 1, '2024-06-03'),
    (12, 82461357, 9.5, 12, 'Dinheiro', 2, '2024-06-04'),
    (13, 75315986, 21, 8, 'MBWay', 3, '2024-06-05'),
    (14, 36985214, 7.8, 10, 'Cartão de Débito', 4, '2024-06-06'),
    (15, 74125896, 16, 14, 'MBWay', 5, '2024-06-07'),
    (16, 98765432, 11, 8, 'Cartão de Crédito', 1, '2024-06-08'),
    (17, 45678912, 9.5, 10, 'Dinheiro', 2, '2024-06-09'),
    (18, 18041804, 22, 6, 'MBWay', 3, '2024-06-10'),
    (19, 75395128, 8.7, 9, 'Cartão de Débito', 4, '2024-06-11'),
    (20, 36925814, 15, 12, 'MBWay', 5, '2024-06-12'),
    (21, 95135786, 19, 14, 'Cartão de Crédito', 1, '2024-06-13'),
    (22, 82461357, 10.5, 10, 'Dinheiro', 2, '2024-06-14'),
    (23, 75315986, 25, 8, 'MBWay', 3, '2024-06-15'),
    (24, 36985214, 6.8, 10, 'Cartão de Débito', 4, '2024-06-16'),
    (25, 74125896, 17, 14, 'MBWay', 5, '2024-06-17');
