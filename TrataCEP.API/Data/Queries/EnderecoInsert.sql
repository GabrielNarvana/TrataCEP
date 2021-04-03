INSERT INTO ENDERECO (
    DataCriacao,
    CEP,
    Logradouro,
    Complemento,
    Bairro,
    Localidade ,
    UF,
    IBGE,
    GIA,
    DDD,
    Siafi)
values(
    @DataCriacao,
    @CEP,
    @Logradouro,
    @Complemento,
    @Bairro,
    @Localidade ,
    @UF,
    @IBGE,
    @GIA,
    @DDD,
    @Siafi)
    RETURNING Id