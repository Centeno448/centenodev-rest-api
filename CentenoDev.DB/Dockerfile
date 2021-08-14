#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM postgres:13

WORKDIR /

COPY ["CentenoDev.DB/centenodev_db.sql", "/docker-entrypoint-initdb.d"]