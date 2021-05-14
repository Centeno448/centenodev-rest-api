--
-- PostgreSQL database dump
--

-- Dumped from database version 13.2 (Debian 13.2-1.pgdg100+1)
-- Dumped by pg_dump version 13.2

-- Started on 2021-05-14 15:44:49

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
-- TOC entry 2974 (class 1262 OID 16384)
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

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 203 (class 1259 OID 16398)
-- Name: Account; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Account" (
    "AccountId" integer NOT NULL,
    "Guid" uuid NOT NULL,
    "Username" character varying(50) NOT NULL,
    "Password" character varying(64) NOT NULL,
    "IsAdmin" bit(1) DEFAULT '0'::"bit" NOT NULL
);


ALTER TABLE public."Account" OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 16396)
-- Name: Account_accountId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Account_accountId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Account_accountId_seq" OWNER TO postgres;

--
-- TOC entry 2977 (class 0 OID 0)
-- Dependencies: 202
-- Name: Account_accountId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Account_accountId_seq" OWNED BY public."Account"."AccountId";


--
-- TOC entry 207 (class 1259 OID 16436)
-- Name: Attachment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Attachment" (
    "AttachmentId" integer NOT NULL,
    "ProjectId" integer NOT NULL,
    "Url" character varying(500) NOT NULL,
    "Guid" uuid NOT NULL
);


ALTER TABLE public."Attachment" OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 16434)
-- Name: Attachment_AttachmentId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Attachment_AttachmentId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Attachment_AttachmentId_seq" OWNER TO postgres;

--
-- TOC entry 2980 (class 0 OID 0)
-- Dependencies: 206
-- Name: Attachment_AttachmentId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Attachment_AttachmentId_seq" OWNED BY public."Attachment"."AttachmentId";


--
-- TOC entry 205 (class 1259 OID 16423)
-- Name: Lesson; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Lesson" (
    "LessonId" integer NOT NULL,
    "ProjectId" integer NOT NULL,
    "Content" character varying(200),
    "Guid" uuid NOT NULL
);


ALTER TABLE public."Lesson" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 16421)
-- Name: Lesson_LessonId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Lesson_LessonId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Lesson_LessonId_seq" OWNER TO postgres;

--
-- TOC entry 2983 (class 0 OID 0)
-- Dependencies: 204
-- Name: Lesson_LessonId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Lesson_LessonId_seq" OWNED BY public."Lesson"."LessonId";


--
-- TOC entry 201 (class 1259 OID 16387)
-- Name: Project; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Project" (
    "ProjectId" integer NOT NULL,
    "Guid" uuid NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Description" character varying(250) NOT NULL,
    "GitRepo" character varying(500),
    "ProdLink" character varying(500),
    "IsPersonal" boolean DEFAULT false NOT NULL
);


ALTER TABLE public."Project" OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 16385)
-- Name: Project_projectId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Project_projectId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Project_projectId_seq" OWNER TO postgres;

--
-- TOC entry 2986 (class 0 OID 0)
-- Dependencies: 200
-- Name: Project_projectId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Project_projectId_seq" OWNED BY public."Project"."ProjectId";


--
-- TOC entry 2825 (class 2604 OID 16401)
-- Name: Account AccountId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Account" ALTER COLUMN "AccountId" SET DEFAULT nextval('public."Account_accountId_seq"'::regclass);


--
-- TOC entry 2828 (class 2604 OID 16439)
-- Name: Attachment AttachmentId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Attachment" ALTER COLUMN "AttachmentId" SET DEFAULT nextval('public."Attachment_AttachmentId_seq"'::regclass);


--
-- TOC entry 2827 (class 2604 OID 16426)
-- Name: Lesson LessonId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Lesson" ALTER COLUMN "LessonId" SET DEFAULT nextval('public."Lesson_LessonId_seq"'::regclass);


--
-- TOC entry 2823 (class 2604 OID 16390)
-- Name: Project ProjectId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Project" ALTER COLUMN "ProjectId" SET DEFAULT nextval('public."Project_projectId_seq"'::regclass);


--
-- TOC entry 2832 (class 2606 OID 16404)
-- Name: Account Account_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Account"
    ADD CONSTRAINT "Account_pkey" PRIMARY KEY ("AccountId");


--
-- TOC entry 2836 (class 2606 OID 16444)
-- Name: Attachment Attachment_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Attachment"
    ADD CONSTRAINT "Attachment_pkey" PRIMARY KEY ("AttachmentId");


--
-- TOC entry 2834 (class 2606 OID 16428)
-- Name: Lesson Lesson_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Lesson"
    ADD CONSTRAINT "Lesson_pkey" PRIMARY KEY ("LessonId");


--
-- TOC entry 2830 (class 2606 OID 16395)
-- Name: Project Project_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Project"
    ADD CONSTRAINT "Project_pkey" PRIMARY KEY ("ProjectId");


--
-- TOC entry 2838 (class 2606 OID 16445)
-- Name: Attachment Attachment_ProjectId_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Attachment"
    ADD CONSTRAINT "Attachment_ProjectId_FK" FOREIGN KEY ("ProjectId") REFERENCES public."Project"("ProjectId");


--
-- TOC entry 2837 (class 2606 OID 16429)
-- Name: Lesson Lesson_ProjectId_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Lesson"
    ADD CONSTRAINT "Lesson_ProjectId_FK" FOREIGN KEY ("ProjectId") REFERENCES public."Project"("ProjectId");


--
-- TOC entry 2975 (class 0 OID 0)
-- Dependencies: 2974
-- Name: DATABASE centenodev_db; Type: ACL; Schema: -; Owner: postgres
--

GRANT CONNECT ON DATABASE centenodev_db TO centenodev_api;


--
-- TOC entry 2976 (class 0 OID 0)
-- Dependencies: 203
-- Name: TABLE "Account"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Account" TO centenodev_api;


--
-- TOC entry 2978 (class 0 OID 0)
-- Dependencies: 202
-- Name: SEQUENCE "Account_accountId_seq"; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public."Account_accountId_seq" TO centenodev_api;


--
-- TOC entry 2979 (class 0 OID 0)
-- Dependencies: 207
-- Name: TABLE "Attachment"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Attachment" TO centenodev_api;


--
-- TOC entry 2981 (class 0 OID 0)
-- Dependencies: 206
-- Name: SEQUENCE "Attachment_AttachmentId_seq"; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public."Attachment_AttachmentId_seq" TO centenodev_api;


--
-- TOC entry 2982 (class 0 OID 0)
-- Dependencies: 205
-- Name: TABLE "Lesson"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Lesson" TO centenodev_api;


--
-- TOC entry 2984 (class 0 OID 0)
-- Dependencies: 204
-- Name: SEQUENCE "Lesson_LessonId_seq"; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public."Lesson_LessonId_seq" TO centenodev_api;


--
-- TOC entry 2985 (class 0 OID 0)
-- Dependencies: 201
-- Name: TABLE "Project"; Type: ACL; Schema: public; Owner: postgres
--

GRANT SELECT,INSERT,REFERENCES,DELETE,UPDATE ON TABLE public."Project" TO centenodev_api;


--
-- TOC entry 2987 (class 0 OID 0)
-- Dependencies: 200
-- Name: SEQUENCE "Project_projectId_seq"; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public."Project_projectId_seq" TO centenodev_api;


-- Completed on 2021-05-14 15:44:50

--
-- PostgreSQL database dump complete
--

