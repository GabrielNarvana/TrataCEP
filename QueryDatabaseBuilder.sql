CREATE TABLE ENDERECO (
   Id serial PRIMARY KEY,
   DataCriacao TIMESTAMP NOT NULL,
   CEP VARCHAR(8) NOT NULL,
   Logradouro VARCHAR (250),
   Complemento VARCHAR(250),
   Bairro VARCHAR(50),
   Localidade VARCHAR (100),
   UF VARCHAR(2),
   IBGE INTEGER,
   GIA INTEGER,
   DD INTEGER,
   Siafi INTEGER
);