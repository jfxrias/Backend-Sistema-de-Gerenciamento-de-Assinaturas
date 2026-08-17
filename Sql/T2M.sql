--
-- PostgreSQL database dump
--

\restrict sJVcPxJOaC5wpJ6bZAxyPAM798RlS3JpqCWubjvL6EgYgjlDp0eO0ata83sZshx

-- Dumped from database version 18.3
-- Dumped by pg_dump version 18.3

-- Started on 2026-08-16 22:33:42

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
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
-- TOC entry 220 (class 1259 OID 73769)
-- Name: assinaturas; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.assinaturas (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    nome character varying(50) CONSTRAINT assinaturas_tipo_not_null NOT NULL,
    maxdependentes integer NOT NULL,
    maxtelas integer NOT NULL,
    valor numeric(10,2) NOT NULL
);


ALTER TABLE public.assinaturas OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 73786)
-- Name: dependentes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.dependentes (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    assinanteid uuid NOT NULL,
    nome character varying(100) NOT NULL,
    email character varying(100),
    senha character varying(255)
);


ALTER TABLE public.dependentes OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 73757)
-- Name: usuarios; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usuarios (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    nome character varying(100) NOT NULL,
    email character varying(100) NOT NULL,
    senha character varying(255) NOT NULL,
    assinaturaid uuid
);


ALTER TABLE public.usuarios OWNER TO postgres;

--
-- TOC entry 5026 (class 0 OID 73769)
-- Dependencies: 220
-- Data for Name: assinaturas; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.assinaturas (id, nome, maxdependentes, maxtelas, valor) FROM stdin;
550e8400-e29b-41d4-a716-446655440000	Assinatura 1	2	1	29.90
660e8400-e29b-41d4-a716-446655440001	Assinatura 2	3	2	59.90
770e8400-e29b-41d4-a716-446655440002	Assinatura 3	5	4	99.90
\.


--
-- TOC entry 5027 (class 0 OID 73786)
-- Dependencies: 221
-- Data for Name: dependentes; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.dependentes (id, assinanteid, nome, email, senha) FROM stdin;
063844ff-2caf-4d63-9729-83810ceaf3f2	063844ff-2caf-4d63-9729-83810ceaf3f2	João Gabriel	joao@gmail.com	123
\.


--
-- TOC entry 5025 (class 0 OID 73757)
-- Dependencies: 219
-- Data for Name: usuarios; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.usuarios (id, nome, email, senha, assinaturaid) FROM stdin;
00000000-0000-0000-0000-000000000000	Rafaela dos Santos	rafaela@email.com	123	660e8400-e29b-41d4-a716-446655440001
063844ff-2caf-4d63-9729-83810ceaf3f2	Breno	breno@email.com	123	770e8400-e29b-41d4-a716-446655440002
\.


--
-- TOC entry 4871 (class 2606 OID 73780)
-- Name: assinaturas assinaturas_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assinaturas
    ADD CONSTRAINT assinaturas_pkey PRIMARY KEY (id);


--
-- TOC entry 4873 (class 2606 OID 73796)
-- Name: dependentes dependentes_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.dependentes
    ADD CONSTRAINT dependentes_email_key UNIQUE (email);


--
-- TOC entry 4875 (class 2606 OID 73794)
-- Name: dependentes dependentes_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.dependentes
    ADD CONSTRAINT dependentes_pkey PRIMARY KEY (id);


--
-- TOC entry 4867 (class 2606 OID 73768)
-- Name: usuarios usuarios_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuarios
    ADD CONSTRAINT usuarios_email_key UNIQUE (email);


--
-- TOC entry 4869 (class 2606 OID 73766)
-- Name: usuarios usuarios_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuarios
    ADD CONSTRAINT usuarios_pkey PRIMARY KEY (id);


--
-- TOC entry 4877 (class 2606 OID 73797)
-- Name: dependentes dependentes_assinanteid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.dependentes
    ADD CONSTRAINT dependentes_assinanteid_fkey FOREIGN KEY (assinanteid) REFERENCES public.usuarios(id) ON DELETE CASCADE;


--
-- TOC entry 4876 (class 2606 OID 81948)
-- Name: usuarios fk_usuario_assinatura; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuarios
    ADD CONSTRAINT fk_usuario_assinatura FOREIGN KEY (assinaturaid) REFERENCES public.assinaturas(id);


-- Completed on 2026-08-16 22:33:43

--
-- PostgreSQL database dump complete
--

\unrestrict sJVcPxJOaC5wpJ6bZAxyPAM798RlS3JpqCWubjvL6EgYgjlDp0eO0ata83sZshx

