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
('Mariana Silva', 58214736, 916243785),
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
('Filipa Carvalho', 36985214, 931475869),
('Hugo Rodrigues', 74125896, 932587146),
('Vera Santos', 58214736, 916243785),
('David Oliveira', 36914725, 938571624),
('Andreia Costa', 15948762, 935824716),
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
(6, 'Quinta do Sol Nascente', 'Praça da Liberdade, 30'),
(7, 'Quinta da Primavera', 'Rua dos L�rios, 15'),
(8, 'Quinta do Sol Poente', 'Avenida das Oliveiras, 25'),
(9, 'Logística Ágil', 'Avenida das Entregas Eficientes, 60'),
(10, 'Fretes Seguros', 'Travessa dos Transportes Seguros, 70'),
(11, 'Transportes Velozes LTDA', 'Rua das Rotas R�pidas, 80'),
(12, 'Log�stica Global', 'Avenida da Efici�ncia Log�stica, 90'),
(13, 'Entrega Segura', 'Travessa da Entrega Certa, 100'),
(14, 'Transportadora �gil Express', 'Rua da Entrega Expressa, 110'),
(15, 'Fretes R�pidos SA', 'Avenida dos Fretes, 120'),
(16, 'Log�stica Integrada', 'Travessa da Log�stica, 130'),
(17, 'Supermercado Central', 'Rua Principal, 250'),
(18, 'Loja do Bairro', 'Avenida Central, 260'),
(19, 'Casa dos Utens�lios', 'Travessa das Utilidades, 270'),
(20, 'Continente', 'Rua das Novidades, 280'),
(21, 'Armaz�m Geral', 'Avenida das Mercadorias, 290'),
(22, 'Loja do Com�rcio', 'Travessa do Com�rcio, 300'),
(23, 'Hipermercado Mega', 'Rua da Economia, 310'),
(24, 'Pingo Doce', 'Avenida da Variedade, 320');

INSERT INTO AgroTrack_Quinta(Codigo_quinta, Empresa_Id_Empresa) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8);

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
(6, 2, '1CAB', 6),
(6, 3, '2CAB', 6),
(7, 1, '1PAT', 7),
(7, 2, '2PAT', 7),
(8, 1, '1GAN', 8),
(8, 2, '2GAN', 8);


INSERT INTO AgroTrack_Planta(Id_planta, Tipo, Estacao) VALUES
(1,'Macieira', 'Primavera'),
(2,'Alface', 'Verão'),
(3,'Macieira', 'Verão'),
(4,'Tomate', 'Outono'),
(5,'Laranjeira', 'Todas'),
(6, 'Pereira', 'Primavera'),
(7,'Alface', 'Primavera'),
(8,'Tomate', 'Primavera'),
(9,'Milho', 'Verão'),
(10,'Cerejeira', 'Verão'),
(11,'Melão', 'Verão'),
(12,'Bananeira', 'Todas'),
(14, 'Cenoura', 'Inverno'),
(15, 'Abóbora', 'Outono'),
(16, 'Morangueiro', 'Primavera'),
(17, 'Videira', 'Verão'),
(18, 'Feijão', 'Verão'),
(19, 'Cebola', 'Inverno'),
(20, 'Batata', 'Inverno');

INSERT INTO AgroTrack_Quinta_Planta(Empresa_Id_Empresa, Lote, Id_Planta) VALUES
(1, 'Lote_A', 1),   -- Macieira na Empresa 1
(1, 'Lote_B', 5),   -- Laranjeira na Empresa 1
(2, 'Lote_C', 2),   -- Alface na Empresa 2
(2, 'Lote_D', 6),   -- Pereira na Empresa 2
(3, 'Lote_E', 3),   -- Macieira na Empresa 3
(3, 'Lote_F', 8),   -- Tomate na Empresa 3
(4, 'Lote_G', 4),   -- Tomate na Empresa 4
(4, 'Lote_H', 9),   -- Milho na Empresa 4
(5, 'Lote_I', 7),   -- Alface na Empresa 5
(5, 'Lote_J', 10),  -- Cerejeira na Empresa 5
(6, 'Lote_K', 11),   -- Mel�o na Empresa 6
(6, 'Lote_L', 12),   -- Bananeira na Empresa 6
(7, 'Lote_M', 13),   -- Cenoura na Empresa 7
(7, 'Lote_N', 14),   -- Cenoura na Empresa 7
(8, 'Lote_O', 15),   -- Ab�bora na Empresa 8
(8, 'Lote_P', 16);   -- Morangueiro na Empresa 8


INSERT INTO AgroTrack_Cliente(Pessoa_N_CartaoCidadao) VALUES --NAO SEI 

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
(2, 36985214, 5),
(1, 15948762, 6),
(2, 36914725, 6),
(1, 45612378, 7),
(2, 58214740, 7),
(1, 36914728, 8),
(2, 15948766, 8);

INSERT INTO AgroTrack_Produto(Nome, Quantidade, Id_origem, Tipo_de_Produto, Codigo, Preco, Data_de_validade, Taxa_de_iva, Unidade_medida, Agricultor_Pessoa_N_CartaoCidadao

INSERT INTO AgroTrack_Contrato(
    Date_str, 
    Date_end, 
    Descricao, 
    Salario, 
    ID, 
    Agricultor_Pessoa_N_CartaoCidadao
) SELECT
    '2024-01-01', -- Data de in�cio fixa
    '2024-12-31', -- Data de fim fixa
    'Contrato Padrão', -- Descri��o do contrato
    1000, -- Sal�rio fixo
    Id_Trabalhador, -- ID do agricultor
    Pessoa_N_CartaoCidadao -- N�mero do cart�o do cidad�o do agricultor
FROM AgroTrack_Agricultor;

INSERT INTO AgroTrack_Retalhistas VALUES
	(17),
	(18),
	(19),
	(20),
	(21),
	(22),
	(23),
	(24);

INSERT INTO AgroTrack_Empresa_De_Transportes (Empresa_Id_Empresa) VALUES
	(9),
	(10),
	(11),
	(12),
	(13),
	(14),
	(15),
	(16);

INSERT INTO AgroTrack_Contem (Produto_codigo, Quinta_Empresa_Id_Empresa, Quantidade) VALUES


INSERT INTO AgroTrack_Colhe(Empresa_Id_Empresa,Agricultor_Pessoa_N_CartaoCidadao, Duracao_colheita,Quantidade,Produto_codigo) VALUES
