--
-- PostgreSQL database dump
--

-- Dumped from database version 13.2 (Debian 13.2-1.pgdg100+1)
-- Dumped by pg_dump version 13.2

-- Started on 2021-05-14 17:36:06

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- Role: centenodev_api
-- DROP ROLE centenodev_api;

CREATE ROLE centenodev_api WITH
  LOGIN
  NOSUPERUSER
  NOINHERIT
  NOCREATEDB
  NOCREATEROLE
  NOREPLICATION
  ENCRYPTED PASSWORD 'md5cabc65ca3d4b9ad732d8179b18ae1e6e';

--
-- TOC entry 2977 (class 1262 OID 16385)
-- Name: centenodev_db; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE centenodev_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE centenodev_db OWNER TO postgres;

\connect centenodev_db

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2 (class 3079 OID 16436)
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- TOC entry 2979 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 201 (class 1259 OID 16386)
-- Name: Account; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Account" (
    "Guid" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Username" character varying(50) NOT NULL,
    "Password" character varying(64) NOT NULL,
    "IsAdmin" bit(1) DEFAULT '0'::"bit" NOT NULL
);


ALTER TABLE public."Account" OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 16392)
-- Name: Attachment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Attachment" (
    "Url" character varying(500) NOT NULL,
    "Guid" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "ProjectGuid" uuid NOT NULL
);


ALTER TABLE public."Attachment" OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 16400)
-- Name: Lesson; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Lesson" (
    "Content" character varying(200),
    "Guid" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "ProjectGuid" uuid NOT NULL
);


ALTER TABLE public."Lesson" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 16405)
-- Name: Project; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Project" (
    "Guid" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Description" character varying(250) NOT NULL,
    "GitRepo" character varying(500),
    "ProdLink" character varying(500),
    "IsPersonal" boolean DEFAULT false NOT NULL
);


ALTER TABLE public."Project" OWNER TO postgres;

--
-- TOC entry 2833 (class 2606 OID 16456)
-- Name: Account Account_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Account"
    ADD CONSTRAINT "Account_pkey" PRIMARY KEY ("Guid");


--
-- TOC entry 2835 (class 2606 OID 16449)
-- Name: Attachment Attachment_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Attachment"
    ADD CONSTRAINT "Attachment_pkey" PRIMARY KEY ("Guid");


--
-- TOC entry 2837 (class 2606 OID 16459)
-- Name: Lesson Lesson_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Lesson"
    ADD CONSTRAINT "Lesson_pkey" PRIMARY KEY ("Guid");


--
-- TOC entry 2839 (class 2606 OID 16462)
-- Name: Project Project_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Project"
    ADD CONSTRAINT "Project_pkey" PRIMARY KEY ("Guid");


--
-- TOC entry 2840 (class 2606 OID 16468)
-- Name: Attachment Attachment_ProjectGuid_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Attachment"
    ADD CONSTRAINT "Attachment_ProjectGuid_FK" FOREIGN KEY ("ProjectGuid") REFERENCES public."Project"("Guid") NOT VALID;


--
-- TOC entry 2841 (class 2606 OID 16463)
-- Name: Lesson Lesson_ProjectGuid_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Lesson"
    ADD CONSTRAINT "Lesson_ProjectGuid_FK" FOREIGN KEY ("ProjectGuid") REFERENCES public."Project"("Guid") NOT VALID;


--
-- TOC entry 2978 (class 0 OID 0)
-- Dependencies: 2977
-- Name: DATABASE centenodev_db; Type: ACL; Schema: -; Owner: postgres
--

GRANT CONNECT ON DATABASE centenodev_db TO centenodev_api;


--
-- TOC entry 2980 (class 0 OID 0)
-- Dependencies: 214
-- Name: FUNCTION uuid_generate_v4(); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_generate_v4() TO centenodev_api;


--
-- TOC entry 2981 (class 0 OID 0)
-- Dependencies: 201
-- Name: TABLE "Account"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Account" TO centenodev_api;


--
-- TOC entry 2982 (class 0 OID 0)
-- Dependencies: 202
-- Name: TABLE "Attachment"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Attachment" TO centenodev_api;


--
-- TOC entry 2983 (class 0 OID 0)
-- Dependencies: 203
-- Name: TABLE "Lesson"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Lesson" TO centenodev_api;


--
-- TOC entry 2984 (class 0 OID 0)
-- Dependencies: 204
-- Name: TABLE "Project"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Project" TO centenodev_api;


-- Completed on 2021-05-14 17:36:06

--
-- PostgreSQL database dump complete
--

