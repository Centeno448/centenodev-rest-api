--
-- PostgreSQL database cluster dump
--

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;


--
-- Roles
--

CREATE ROLE centenodev_admin;
ALTER ROLE centenodev_admin WITH NOSUPERUSER INHERIT NOCREATEROLE NOCREATEDB LOGIN NOREPLICATION NOBYPASSRLS PASSWORD 'md51090af6aefba0f655c5a1f1925327d60';
CREATE ROLE centenodev_api;
ALTER ROLE centenodev_api WITH NOSUPERUSER INHERIT NOCREATEROLE NOCREATEDB LOGIN NOREPLICATION NOBYPASSRLS PASSWORD 'md5cabc65ca3d4b9ad732d8179b18ae1e6e';
CREATE ROLE centenodev_app;
ALTER ROLE centenodev_app WITH NOSUPERUSER NOINHERIT NOCREATEROLE NOCREATEDB NOLOGIN NOREPLICATION NOBYPASSRLS;


--
-- Role memberships
--

GRANT centenodev_app TO centenodev_admin GRANTED BY postgres;
GRANT centenodev_app TO centenodev_api GRANTED BY postgres;

--
-- Database "centenodev_db" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 13.3 (Debian 13.3-1.pgdg100+1)
-- Dumped by pg_dump version 13.3 (Debian 13.3-1.pgdg100+1)

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
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Account; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Account" (
    "Guid" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "Username" character varying(50) NOT NULL,
    "Password_digest" character varying(72) NOT NULL,
    "IsAdmin" bit(1) DEFAULT '0'::"bit" NOT NULL
);


ALTER TABLE public."Account" OWNER TO postgres;

--
-- Name: Attachment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Attachment" (
    "Url" character varying(500) NOT NULL,
    "Guid" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "ProjectGuid" uuid NOT NULL
);


ALTER TABLE public."Attachment" OWNER TO postgres;

--
-- Name: Lesson; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Lesson" (
    "Content" character varying(200),
    "Guid" uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "ProjectGuid" uuid NOT NULL
);


ALTER TABLE public."Lesson" OWNER TO postgres;

--
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
-- Name: Account Account_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Account"
    ADD CONSTRAINT "Account_pkey" PRIMARY KEY ("Guid");


--
-- Name: Attachment Attachment_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Attachment"
    ADD CONSTRAINT "Attachment_pkey" PRIMARY KEY ("Guid");


--
-- Name: Lesson Lesson_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Lesson"
    ADD CONSTRAINT "Lesson_pkey" PRIMARY KEY ("Guid");


--
-- Name: Project Project_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Project"
    ADD CONSTRAINT "Project_pkey" PRIMARY KEY ("Guid");


--
-- Name: Account UNIQUE_Account_Username; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Account"
    ADD CONSTRAINT "UNIQUE_Account_Username" UNIQUE ("Username");


--
-- Name: Attachment Attachment_ProjectGuid_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Attachment"
    ADD CONSTRAINT "Attachment_ProjectGuid_FK" FOREIGN KEY ("ProjectGuid") REFERENCES public."Project"("Guid") NOT VALID;


--
-- Name: Lesson Lesson_ProjectGuid_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Lesson"
    ADD CONSTRAINT "Lesson_ProjectGuid_FK" FOREIGN KEY ("ProjectGuid") REFERENCES public."Project"("Guid") NOT VALID;


--
-- Name: DATABASE centenodev_db; Type: ACL; Schema: -; Owner: postgres
--

REVOKE CONNECT,TEMPORARY ON DATABASE centenodev_db FROM PUBLIC;
GRANT CONNECT,TEMPORARY ON DATABASE centenodev_db TO centenodev_app;


--
-- Name: FUNCTION uuid_generate_v1(); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_generate_v1() TO centenodev_app;


--
-- Name: FUNCTION uuid_generate_v1mc(); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_generate_v1mc() TO centenodev_app;


--
-- Name: FUNCTION uuid_generate_v3(namespace uuid, name text); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_generate_v3(namespace uuid, name text) TO centenodev_app;


--
-- Name: FUNCTION uuid_generate_v4(); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_generate_v4() TO centenodev_app;


--
-- Name: FUNCTION uuid_generate_v5(namespace uuid, name text); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_generate_v5(namespace uuid, name text) TO centenodev_app;


--
-- Name: FUNCTION uuid_nil(); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_nil() TO centenodev_app;


--
-- Name: FUNCTION uuid_ns_dns(); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_ns_dns() TO centenodev_app;


--
-- Name: FUNCTION uuid_ns_oid(); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_ns_oid() TO centenodev_app;


--
-- Name: FUNCTION uuid_ns_url(); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_ns_url() TO centenodev_app;


--
-- Name: FUNCTION uuid_ns_x500(); Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON FUNCTION public.uuid_ns_x500() TO centenodev_app;


--
-- Name: TABLE "Account"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Account" TO centenodev_app;


--
-- Name: TABLE "Attachment"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Attachment" TO centenodev_app;


--
-- Name: TABLE "Lesson"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Lesson" TO centenodev_app;


--
-- Name: TABLE "Project"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Project" TO centenodev_app;


--
-- PostgreSQL database dump complete
--


