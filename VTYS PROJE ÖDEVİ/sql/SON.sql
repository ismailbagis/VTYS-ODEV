--
-- PostgreSQL database dump
--

-- Dumped from database version 15.5
-- Dumped by pg_dump version 15.5

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
-- Name: acilis(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.acilis() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO yonetici (yonetici_id,yonetici_adi, sirket_acilis_tarihi)
    VALUES (yonetici_id,NEW.yonetici_adi, CURRENT_TIMESTAMP);
    
    RETURN NULL;
END;
$$;


ALTER FUNCTION public.acilis() OWNER TO postgres;

--
-- Name: calisan_say(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.calisan_say() RETURNS integer
    LANGUAGE plpgsql
    AS $$
DECLARE
    total_count INTEGER;
BEGIN
    -- Kişi sayısını bul
    SELECT COUNT(*) INTO total_count FROM calisan;

    -- Sonucu döndür
    RETURN total_count;
END;
$$;


ALTER FUNCTION public.calisan_say() OWNER TO postgres;

--
-- Name: mudur_say(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mudur_say() RETURNS integer
    LANGUAGE plpgsql
    AS $$
DECLARE
    total_count INTEGER;
BEGIN
    -- Kişi sayısını bul
    SELECT COUNT(*) INTO total_count FROM mudur;

    -- Sonucu döndür
    RETURN total_count;
END;
$$;


ALTER FUNCTION public.mudur_say() OWNER TO postgres;

--
-- Name: ofismemuru_say(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.ofismemuru_say() RETURNS integer
    LANGUAGE plpgsql
    AS $$
DECLARE
    total_count INTEGER;
BEGIN
    -- Kişi sayısını bul
    SELECT COUNT(*) INTO total_count FROM ofismemuru;

    -- Sonucu döndür
    RETURN total_count;
END;
$$;


ALTER FUNCTION public.ofismemuru_say() OWNER TO postgres;

--
-- Name: otopark_sayisi(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.otopark_sayisi() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        UPDATE sirket
        SET otopark_sayisi = otopark_sayisi + 1
        WHERE sirket_id = NEW.sirket_id;
    ELSIF TG_OP = 'DELETE' THEN
        UPDATE sirket
        SET otopark_sayisi = otopark_sayisi - 1
        WHERE sirket_id = OLD.sirket_id;
    END IF;

    RETURN NULL;
END;
$$;


ALTER FUNCTION public.otopark_sayisi() OWNER TO postgres;

--
-- Name: say(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.say() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
begin
update "sirket" set sayi=sayi+1 from "sube" where "sirket"."sirket_id"= public."sube"."sirket_id";
return new;
end;
$$;


ALTER FUNCTION public.say() OWNER TO postgres;

--
-- Name: siparis_say(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.siparis_say() RETURNS integer
    LANGUAGE plpgsql
    AS $$
DECLARE
    total_count INTEGER;
BEGIN
    -- Kişi sayısını bul
    SELECT COUNT(*) INTO total_count FROM siparis;

    -- Sonucu döndür
    RETURN total_count;
END;
$$;


ALTER FUNCTION public.siparis_say() OWNER TO postgres;

--
-- Name: sirket_acilis_tarihi_guncelle(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sirket_acilis_tarihi_guncelle() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
  UPDATE Yonetici
  SET sirket_acilis_tarihi = NEW.sirket_acilis_tarihi
  WHERE yonetici_id = NEW.yonetici_id;
  RETURN NEW;
END;
$$;


ALTER FUNCTION public.sirket_acilis_tarihi_guncelle() OWNER TO postgres;

--
-- Name: sirket_ekleme_fonksiyonu(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sirket_ekleme_fonksiyonu() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Yonetici tablosundan ilgili yonetici_adi'nin sirket_acilis_tarihi alınıyor
    SELECT sirket_acilis_tarihi INTO NEW.sirket_acilis_tarihi
    FROM yonetici
    WHERE yonetici.yonetici_id = NEW.yonetici_id;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.sirket_ekleme_fonksiyonu() OWNER TO postgres;

--
-- Name: sofor_say(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sofor_say() RETURNS integer
    LANGUAGE plpgsql
    AS $$
DECLARE
    total_count INTEGER;
BEGIN
    -- Kişi sayısını bul
    SELECT COUNT(*) INTO total_count FROM sofor;

    -- Sonucu döndür
    RETURN total_count;
END;
$$;


ALTER FUNCTION public.sofor_say() OWNER TO postgres;

--
-- Name: sube_say(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sube_say() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
begin
update sirket set sube_sayisi=sube_sayisi+1;
return new;
end;
$$;


ALTER FUNCTION public.sube_say() OWNER TO postgres;

--
-- Name: sube_sayisi(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sube_sayisi() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        UPDATE sirket
        SET sube_sayisi = sube_sayisi + 1
        WHERE sirket_id = NEW.sirket_id;
    ELSIF TG_OP = 'DELETE' THEN
        UPDATE sirket
        SET sube_sayisi = sube_sayisi - 1
        WHERE sirket_id = OLD.sirket_id;
    END IF;

    RETURN NULL;
END;
$$;


ALTER FUNCTION public.sube_sayisi() OWNER TO postgres;

--
-- Name: tamirci_say(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tamirci_say() RETURNS integer
    LANGUAGE plpgsql
    AS $$
DECLARE
    total_count INTEGER;
BEGIN
    -- Kişi sayısını bul
    SELECT COUNT(*) INTO total_count FROM tamirci;

    -- Sonucu döndür
    RETURN total_count;
END;
$$;


ALTER FUNCTION public.tamirci_say() OWNER TO postgres;

--
-- Name: temizlikelemani_say(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.temizlikelemani_say() RETURNS integer
    LANGUAGE plpgsql
    AS $$
DECLARE
    total_count INTEGER;
BEGIN
    -- Kişi sayısını bul
    SELECT COUNT(*) INTO total_count FROM temizlikelemani;

    -- Sonucu döndür
    RETURN total_count;
END;
$$;


ALTER FUNCTION public.temizlikelemani_say() OWNER TO postgres;

--
-- Name: update_sube_say(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.update_sube_say() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
  UPDATE sube
  SET sube_sayisi = sube_sayisi + 1;
  
  RETURN NEW;
END;
$$;


ALTER FUNCTION public.update_sube_say() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: calisan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.calisan (
    calisan_id integer NOT NULL,
    calisan_adi character varying(50) NOT NULL,
    sirket_id integer NOT NULL,
    yas integer,
    kilo integer
);


ALTER TABLE public.calisan OWNER TO postgres;

--
-- Name: fatura; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.fatura (
    fatura_id integer NOT NULL,
    fatura_tutari integer NOT NULL,
    fatura_etiket text NOT NULL,
    fatura_tarihi text,
    musteri_id integer NOT NULL,
    siparis_id integer NOT NULL
);


ALTER TABLE public.fatura OWNER TO postgres;

--
-- Name: iletisim; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.iletisim (
    iletisim_id integer NOT NULL,
    iletisim_tipi text NOT NULL,
    iletisim_adresi character varying NOT NULL,
    musteri_id integer NOT NULL
);


ALTER TABLE public.iletisim OWNER TO postgres;

--
-- Name: mudur; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.mudur (
    calisan_id integer,
    mudur_maas integer,
    mudur_izin character varying
)
INHERITS (public.calisan);


ALTER TABLE public.mudur OWNER TO postgres;

--
-- Name: musteri; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.musteri (
    musteri_id integer NOT NULL,
    musteri_adi character varying(50) NOT NULL,
    sirket_id integer NOT NULL
);


ALTER TABLE public.musteri OWNER TO postgres;

--
-- Name: ofismemuru; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ofismemuru (
    calisan_id integer,
    ofis_maas integer,
    ofis_izin character varying
)
INHERITS (public.calisan);


ALTER TABLE public.ofismemuru OWNER TO postgres;

--
-- Name: otopark; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.otopark (
    otopark_id integer NOT NULL,
    otopark_adi character varying(50) NOT NULL,
    sirket_id integer NOT NULL
);


ALTER TABLE public.otopark OWNER TO postgres;

--
-- Name: siparis; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.siparis (
    siparis_id integer NOT NULL,
    musteri_id integer NOT NULL,
    tir_id integer NOT NULL,
    siparis_tutari integer NOT NULL,
    yukleme character varying NOT NULL,
    bosaltma character varying NOT NULL,
    siparis_tarihi text,
    siparis_bilgi text
);


ALTER TABLE public.siparis OWNER TO postgres;

--
-- Name: sirket; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sirket (
    sirket_id integer NOT NULL,
    sirket_adi character varying(50) NOT NULL,
    yonetici_id integer NOT NULL,
    sube_sayisi integer DEFAULT 0,
    otopark_sayisi integer DEFAULT 0
);


ALTER TABLE public.sirket OWNER TO postgres;

--
-- Name: sofor; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sofor (
    calisan_id integer,
    tir_id integer NOT NULL,
    sofor_maas integer,
    sofor_izin character varying(15)
)
INHERITS (public.calisan);


ALTER TABLE public.sofor OWNER TO postgres;

--
-- Name: sube; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sube (
    sube_id integer NOT NULL,
    sube_adi character varying(50) NOT NULL,
    sirket_id integer NOT NULL
);


ALTER TABLE public.sube OWNER TO postgres;

--
-- Name: tamirci; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tamirci (
    calisan_id integer,
    tamir_maas integer,
    tamir_izin character varying
)
INHERITS (public.calisan);


ALTER TABLE public.tamirci OWNER TO postgres;

--
-- Name: temizlikelemani; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.temizlikelemani (
    calisan_id integer,
    temizlik_elemani_maas integer,
    temizlik_elemani_izin character varying
)
INHERITS (public.calisan);


ALTER TABLE public.temizlikelemani OWNER TO postgres;

--
-- Name: tir; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tir (
    tir_id integer NOT NULL,
    tir_marka character varying(50) NOT NULL,
    tir_model character varying(50) NOT NULL,
    tir_plaka character varying(50) NOT NULL,
    sirket_id integer NOT NULL
);


ALTER TABLE public.tir OWNER TO postgres;

--
-- Name: yonetici; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.yonetici (
    yonetici_id integer NOT NULL,
    yonetici_adi character varying(50) NOT NULL
);


ALTER TABLE public.yonetici OWNER TO postgres;

--
-- Data for Name: calisan; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.calisan (calisan_id, calisan_adi, sirket_id, yas, kilo) FROM stdin;
\.


--
-- Data for Name: fatura; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.fatura (fatura_id, fatura_tutari, fatura_etiket, fatura_tarihi, musteri_id, siparis_id) FROM stdin;
23	34525	ALİ 1	234523	23	3
24	4545	ALİ 1	545	23	3
\.


--
-- Data for Name: iletisim; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.iletisim (iletisim_id, iletisim_tipi, iletisim_adresi, musteri_id) FROM stdin;
\.


--
-- Data for Name: mudur; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.mudur (calisan_id, calisan_adi, sirket_id, yas, kilo, mudur_maas, mudur_izin) FROM stdin;
6	emir	1	12	12	12	12
7	emir	1	12	12	12	12
\.


--
-- Data for Name: musteri; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.musteri (musteri_id, musteri_adi, sirket_id) FROM stdin;
23	ALİ TAVUZ	1
233	HİHMET AYAZ	1
2334	AHMET AKAN	1
\.


--
-- Data for Name: ofismemuru; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.ofismemuru (calisan_id, calisan_adi, sirket_id, yas, kilo, ofis_maas, ofis_izin) FROM stdin;
\.


--
-- Data for Name: otopark; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.otopark (otopark_id, otopark_adi, sirket_id) FROM stdin;
1	sad	1
2	dsadsa	2
3	sadasd	1
45	KUBİ PARK	5
\.


--
-- Data for Name: siparis; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.siparis (siparis_id, musteri_id, tir_id, siparis_tutari, yukleme, bosaltma, siparis_tarihi, siparis_bilgi) FROM stdin;
3	23	2	23000	İSTANBUL	HATAY	30.10.2022	EŞYA
1	233	67	24554	ANKARA	MRDİN	30.10.2020	HALI 
\.


--
-- Data for Name: sirket; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sirket (sirket_id, sirket_adi, yonetici_id, sube_sayisi, otopark_sayisi) FROM stdin;
4	asas	9	0	0
3	VARDAR LOJİSTİK	6	1	0
2	x lojsitik	8	0	1
5	GREEN FAST	25	0	1
1	MARDEP LOJİSTİK	8	0	2
\.


--
-- Data for Name: sofor; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sofor (calisan_id, calisan_adi, sirket_id, yas, kilo, tir_id, sofor_maas, sofor_izin) FROM stdin;
8	ÜNAL BULUT	3	23	90	67	90000	SALI
\.


--
-- Data for Name: sube; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sube (sube_id, sube_adi, sirket_id) FROM stdin;
3	da	3
\.


--
-- Data for Name: tamirci; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tamirci (calisan_id, calisan_adi, sirket_id, yas, kilo, tamir_maas, tamir_izin) FROM stdin;
1	emir	1	12	12	12	12
3	emir	1	12	12	12	12
\.


--
-- Data for Name: temizlikelemani; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.temizlikelemani (calisan_id, calisan_adi, sirket_id, yas, kilo, temizlik_elemani_maas, temizlik_elemani_izin) FROM stdin;
2	emir	1	12	12	12	12
4	emir	1	12	12	12	12
5	emir	1	12	12	12	12
\.


--
-- Data for Name: tir; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tir (tir_id, tir_marka, tir_model, tir_plaka, sirket_id) FROM stdin;
2	MERCEDES	AXOR	47 ACH 6757	1
3	FORD	1848	47 AG 720	1
67	MERCEDES	AXOR	33 ADT 150	1
\.


--
-- Data for Name: yonetici; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.yonetici (yonetici_id, yonetici_adi) FROM stdin;
6	EMİR VARDAR
11	AHMET BAĞIŞ
9	ALİ VARDAR
7	MEHMET VARDAR
3	ABDULKADİR BAĞIŞ
1	MURAT BAĞIŞ
8	MUSTAFA VARDAR
25	KUBİLAY KÖZLEME
\.


--
-- Name: calisan calisan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.calisan
    ADD CONSTRAINT calisan_pkey PRIMARY KEY (calisan_id);


--
-- Name: fatura fatura_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fatura
    ADD CONSTRAINT fatura_pkey PRIMARY KEY (fatura_id);


--
-- Name: iletisim iletisim_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.iletisim
    ADD CONSTRAINT iletisim_pkey PRIMARY KEY (iletisim_id);


--
-- Name: mudur mudur_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.mudur
    ADD CONSTRAINT mudur_pkey PRIMARY KEY (calisan_id);


--
-- Name: musteri musteri_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.musteri
    ADD CONSTRAINT musteri_pkey PRIMARY KEY (musteri_id);


--
-- Name: ofismemuru ofismemuru_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ofismemuru
    ADD CONSTRAINT ofismemuru_pkey PRIMARY KEY (calisan_id);


--
-- Name: otopark otopark_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.otopark
    ADD CONSTRAINT otopark_pkey PRIMARY KEY (otopark_id);


--
-- Name: siparis siparis_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.siparis
    ADD CONSTRAINT siparis_pkey PRIMARY KEY (siparis_id);


--
-- Name: sirket sirket_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sirket
    ADD CONSTRAINT sirket_pkey PRIMARY KEY (sirket_id);


--
-- Name: sofor sofor_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sofor
    ADD CONSTRAINT sofor_pkey PRIMARY KEY (calisan_id);


--
-- Name: sube sube_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sube
    ADD CONSTRAINT sube_pkey PRIMARY KEY (sube_id);


--
-- Name: tamirci tamirci_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tamirci
    ADD CONSTRAINT tamirci_pkey PRIMARY KEY (calisan_id);


--
-- Name: temizlikelemani temizlikelemani_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.temizlikelemani
    ADD CONSTRAINT temizlikelemani_pkey PRIMARY KEY (calisan_id);


--
-- Name: tir tir_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tir
    ADD CONSTRAINT tir_pkey PRIMARY KEY (tir_id);


--
-- Name: yonetici yonetici_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.yonetici
    ADD CONSTRAINT yonetici_pkey PRIMARY KEY (yonetici_id);


--
-- Name: fki_ALTER TABLE IF EXISTS public.fatura     ADD CONSTRAINT fatu; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_ALTER TABLE IF EXISTS public.fatura     ADD CONSTRAINT fatu" ON public.sirket USING btree (yonetici_id);


--
-- Name: fki_c; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_c ON public.calisan USING btree (sirket_id);


--
-- Name: fki_fatura_musteri_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_fatura_musteri_id_fkey ON public.fatura USING btree (fatura_id);


--
-- Name: fki_fatura_siparis_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_fatura_siparis_id_fkey ON public.fatura USING btree (siparis_id);


--
-- Name: fki_iletisim_musteri_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_iletisim_musteri_id_fkey ON public.iletisim USING btree (musteri_id);


--
-- Name: fki_mudur_calisan_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_mudur_calisan_id_fkey ON public.mudur USING btree (calisan_id);


--
-- Name: fki_musteri_sirket_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_musteri_sirket_id_fkey ON public.musteri USING btree (sirket_id);


--
-- Name: fki_ofissmemuru_calisan_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_ofissmemuru_calisan_id_fkey ON public.ofismemuru USING btree (calisan_id);


--
-- Name: fki_otopark_sirket_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_otopark_sirket_id_fkey ON public.otopark USING btree (sirket_id);


--
-- Name: fki_siparis_musteri_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_siparis_musteri_id_fkey ON public.siparis USING btree (musteri_id);


--
-- Name: fki_siparis_tir_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_siparis_tir_id_fkey ON public.siparis USING btree (tir_id);


--
-- Name: fki_sofor_calisan_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_sofor_calisan_id_fkey ON public.sofor USING btree (calisan_id);


--
-- Name: fki_sofor_tir_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_sofor_tir_id_fkey ON public.sofor USING btree (tir_id);


--
-- Name: fki_sube_sirket_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_sube_sirket_id_fkey ON public.sube USING btree (sirket_id);


--
-- Name: fki_tamirci_calisan_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_tamirci_calisan_id_fkey ON public.tamirci USING btree (calisan_id);


--
-- Name: fki_temizlikelemani_calisan_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_temizlikelemani_calisan_id_fkey ON public.temizlikelemani USING btree (calisan_id);


--
-- Name: fki_tir_sirket_id_fkey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_tir_sirket_id_fkey ON public.tir USING btree (sirket_id);


--
-- Name: otopark otopark_after_delete; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER otopark_after_delete AFTER DELETE ON public.otopark FOR EACH ROW EXECUTE FUNCTION public.otopark_sayisi();


--
-- Name: otopark otopark_after_insert; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER otopark_after_insert AFTER INSERT ON public.otopark FOR EACH ROW EXECUTE FUNCTION public.otopark_sayisi();


--
-- Name: sube sube_after_delete; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER sube_after_delete AFTER DELETE ON public.sube FOR EACH ROW EXECUTE FUNCTION public.sube_sayisi();


--
-- Name: sube sube_after_insert; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER sube_after_insert AFTER INSERT ON public.sube FOR EACH ROW EXECUTE FUNCTION public.sube_sayisi();


--
-- Name: calisan calisan_sirkt_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.calisan
    ADD CONSTRAINT calisan_sirkt_id_fkey FOREIGN KEY (sirket_id) REFERENCES public.sirket(sirket_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: fatura fatura_musteri_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fatura
    ADD CONSTRAINT fatura_musteri_id_fkey FOREIGN KEY (musteri_id) REFERENCES public.musteri(musteri_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: fatura fatura_siparis_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fatura
    ADD CONSTRAINT fatura_siparis_id_fkey FOREIGN KEY (siparis_id) REFERENCES public.siparis(siparis_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: iletisim iletisim_musteri_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.iletisim
    ADD CONSTRAINT iletisim_musteri_id_fkey FOREIGN KEY (musteri_id) REFERENCES public.musteri(musteri_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: mudur mudur_calisan_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.mudur
    ADD CONSTRAINT mudur_calisan_id_fkey FOREIGN KEY (calisan_id) REFERENCES public.mudur(calisan_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: musteri musteri_sirket_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.musteri
    ADD CONSTRAINT musteri_sirket_id_fkey FOREIGN KEY (sirket_id) REFERENCES public.sirket(sirket_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: ofismemuru ofissmemuru_calisan_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ofismemuru
    ADD CONSTRAINT ofissmemuru_calisan_id_fkey FOREIGN KEY (calisan_id) REFERENCES public.ofismemuru(calisan_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: otopark otopark_sirket_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.otopark
    ADD CONSTRAINT otopark_sirket_id_fkey FOREIGN KEY (sirket_id) REFERENCES public.sirket(sirket_id) ON DELETE CASCADE NOT VALID;


--
-- Name: siparis siparis_musteri_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.siparis
    ADD CONSTRAINT siparis_musteri_id_fkey FOREIGN KEY (musteri_id) REFERENCES public.musteri(musteri_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: siparis siparis_tir_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.siparis
    ADD CONSTRAINT siparis_tir_id_fkey FOREIGN KEY (tir_id) REFERENCES public.tir(tir_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: sirket sirket_yonetici_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sirket
    ADD CONSTRAINT sirket_yonetici_id_fkey FOREIGN KEY (yonetici_id) REFERENCES public.yonetici(yonetici_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: sofor sofor_calisan_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sofor
    ADD CONSTRAINT sofor_calisan_id_fkey FOREIGN KEY (calisan_id) REFERENCES public.sofor(calisan_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: sofor sofor_tir_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sofor
    ADD CONSTRAINT sofor_tir_id_fkey FOREIGN KEY (tir_id) REFERENCES public.tir(tir_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: sube sube_sirket_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sube
    ADD CONSTRAINT sube_sirket_id_fkey FOREIGN KEY (sirket_id) REFERENCES public.sirket(sirket_id) ON DELETE CASCADE NOT VALID;


--
-- Name: tamirci tamirci_calisan_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tamirci
    ADD CONSTRAINT tamirci_calisan_id_fkey FOREIGN KEY (calisan_id) REFERENCES public.tamirci(calisan_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: temizlikelemani temizlikelemani_calisan_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.temizlikelemani
    ADD CONSTRAINT temizlikelemani_calisan_id_fkey FOREIGN KEY (calisan_id) REFERENCES public.temizlikelemani(calisan_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: tir tir_sirket_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tir
    ADD CONSTRAINT tir_sirket_id_fkey FOREIGN KEY (sirket_id) REFERENCES public.sirket(sirket_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- PostgreSQL database dump complete
--

