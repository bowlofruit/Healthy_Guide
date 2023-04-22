PGDMP     "    ,                {        !   Culinary_guide_for_healthy_eating    15.2    15.2 h    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16398 !   Culinary_guide_for_healthy_eating    DATABASE     �   CREATE DATABASE "Culinary_guide_for_healthy_eating" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Ukrainian_Ukraine.1252';
 3   DROP DATABASE "Culinary_guide_for_healthy_eating";
                postgres    false            �            1259    16550    allergie    TABLE     a   CREATE TABLE public.allergie (
    userid integer NOT NULL,
    ingredientid integer NOT NULL
);
    DROP TABLE public.allergie;
       public         heap    postgres    false            �            1259    16466    blog    TABLE     S   CREATE TABLE public.blog (
    id integer NOT NULL,
    userid integer NOT NULL
);
    DROP TABLE public.blog;
       public         heap    postgres    false            �            1259    16465    blog_id_seq    SEQUENCE     �   CREATE SEQUENCE public.blog_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.blog_id_seq;
       public          postgres    false    225            �           0    0    blog_id_seq    SEQUENCE OWNED BY     ;   ALTER SEQUENCE public.blog_id_seq OWNED BY public.blog.id;
          public          postgres    false    224            �            1259    16487    blogcategory    TABLE     c   CREATE TABLE public.blogcategory (
    categoryid integer NOT NULL,
    blogid integer NOT NULL
);
     DROP TABLE public.blogcategory;
       public         heap    postgres    false            �            1259    16477    blogcontent    TABLE     �   CREATE TABLE public.blogcontent (
    headline character varying(255) NOT NULL,
    content text NOT NULL,
    blogid integer NOT NULL
);
    DROP TABLE public.blogcontent;
       public         heap    postgres    false            �            1259    16416    category    TABLE     d   CREATE TABLE public.category (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);
    DROP TABLE public.category;
       public         heap    postgres    false            �            1259    16415    categories_id_seq    SEQUENCE     �   CREATE SEQUENCE public.categories_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.categories_id_seq;
       public          postgres    false    219            �           0    0    categories_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.categories_id_seq OWNED BY public.category.id;
          public          postgres    false    218            �            1259    16565    dailybodyweight    TABLE     �   CREATE TABLE public.dailybodyweight (
    userid integer NOT NULL,
    weight double precision NOT NULL,
    date date NOT NULL
);
 #   DROP TABLE public.dailybodyweight;
       public         heap    postgres    false            �            1259    16539 	   dailymenu    TABLE     p   CREATE TABLE public.dailymenu (
    id integer NOT NULL,
    userid integer NOT NULL,
    date date NOT NULL
);
    DROP TABLE public.dailymenu;
       public         heap    postgres    false            �            1259    16538    dailymenu_id_seq    SEQUENCE     �   CREATE SEQUENCE public.dailymenu_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.dailymenu_id_seq;
       public          postgres    false    234            �           0    0    dailymenu_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.dailymenu_id_seq OWNED BY public.dailymenu.id;
          public          postgres    false    233            �            1259    16409 
   ingredient    TABLE     �   CREATE TABLE public.ingredient (
    id integer NOT NULL,
    carbohydrates integer NOT NULL,
    fats integer NOT NULL,
    proteins integer NOT NULL,
    name character varying(255) NOT NULL
);
    DROP TABLE public.ingredient;
       public         heap    postgres    false            �            1259    16408    ingredients_id_seq    SEQUENCE     �   CREATE SEQUENCE public.ingredients_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.ingredients_id_seq;
       public          postgres    false    217            �           0    0    ingredients_id_seq    SEQUENCE OWNED BY     H   ALTER SEQUENCE public.ingredients_id_seq OWNED BY public.ingredient.id;
          public          postgres    false    216            �            1259    16422    ingredientslist    TABLE     j   CREATE TABLE public.ingredientslist (
    recipeid integer NOT NULL,
    ingredientid integer NOT NULL
);
 #   DROP TABLE public.ingredientslist;
       public         heap    postgres    false            �            1259    16400    recipe    TABLE     �   CREATE TABLE public.recipe (
    id integer NOT NULL,
    cookingtime integer NOT NULL,
    servings integer NOT NULL,
    kcal integer NOT NULL
);
    DROP TABLE public.recipe;
       public         heap    postgres    false            �            1259    16399    recipe_id_seq    SEQUENCE     �   CREATE SEQUENCE public.recipe_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.recipe_id_seq;
       public          postgres    false    215            �           0    0    recipe_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.recipe_id_seq OWNED BY public.recipe.id;
          public          postgres    false    214            �            1259    16437    recipecategory    TABLE     g   CREATE TABLE public.recipecategory (
    categoryid integer NOT NULL,
    recipeid integer NOT NULL
);
 "   DROP TABLE public.recipecategory;
       public         heap    postgres    false            �            1259    16590 
   recipeinfo    TABLE     �   CREATE TABLE public.recipeinfo (
    instruction text NOT NULL,
    headline character varying(255),
    recipeid integer NOT NULL
);
    DROP TABLE public.recipeinfo;
       public         heap    postgres    false            �            1259    16575 
   recipelist    TABLE     d   CREATE TABLE public.recipelist (
    dailymenuid integer NOT NULL,
    recipeid integer NOT NULL
);
    DROP TABLE public.recipelist;
       public         heap    postgres    false            �            1259    16510    review    TABLE     �   CREATE TABLE public.review (
    id integer NOT NULL,
    score integer NOT NULL,
    userid integer NOT NULL,
    reviewtypeid integer NOT NULL,
    reviewdate date NOT NULL
);
    DROP TABLE public.review;
       public         heap    postgres    false            �            1259    16509    review_id_seq    SEQUENCE     �   CREATE SEQUENCE public.review_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.review_id_seq;
       public          postgres    false    231            �           0    0    review_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.review_id_seq OWNED BY public.review.id;
          public          postgres    false    230            �            1259    16528    reviewcontent    TABLE     �   CREATE TABLE public.reviewcontent (
    headline character varying(255) NOT NULL,
    content text NOT NULL,
    reviewid integer NOT NULL
);
 !   DROP TABLE public.reviewcontent;
       public         heap    postgres    false            �            1259    16503 
   reviewtype    TABLE     �   CREATE TABLE public.reviewtype (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    targetid integer NOT NULL
);
    DROP TABLE public.reviewtype;
       public         heap    postgres    false            �            1259    16502    reviewtype_id_seq    SEQUENCE     �   CREATE SEQUENCE public.reviewtype_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.reviewtype_id_seq;
       public          postgres    false    229            �           0    0    reviewtype_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.reviewtype_id_seq OWNED BY public.reviewtype.id;
          public          postgres    false    228            �            1259    16453    users    TABLE     �   CREATE TABLE public.users (
    id integer NOT NULL,
    email character varying(255) NOT NULL,
    phonenumber character varying(20) NOT NULL,
    login character varying(255) NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false            �            1259    16452    users_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.users_id_seq;
       public          postgres    false    223            �           0    0    users_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;
          public          postgres    false    222            �           2604    16469    blog id    DEFAULT     b   ALTER TABLE ONLY public.blog ALTER COLUMN id SET DEFAULT nextval('public.blog_id_seq'::regclass);
 6   ALTER TABLE public.blog ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    225    224    225            �           2604    16419    category id    DEFAULT     l   ALTER TABLE ONLY public.category ALTER COLUMN id SET DEFAULT nextval('public.categories_id_seq'::regclass);
 :   ALTER TABLE public.category ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    218    219    219            �           2604    16542    dailymenu id    DEFAULT     l   ALTER TABLE ONLY public.dailymenu ALTER COLUMN id SET DEFAULT nextval('public.dailymenu_id_seq'::regclass);
 ;   ALTER TABLE public.dailymenu ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    233    234    234            �           2604    16412    ingredient id    DEFAULT     o   ALTER TABLE ONLY public.ingredient ALTER COLUMN id SET DEFAULT nextval('public.ingredients_id_seq'::regclass);
 <   ALTER TABLE public.ingredient ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    216    217    217            �           2604    16403 	   recipe id    DEFAULT     f   ALTER TABLE ONLY public.recipe ALTER COLUMN id SET DEFAULT nextval('public.recipe_id_seq'::regclass);
 8   ALTER TABLE public.recipe ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    215    214    215            �           2604    16513 	   review id    DEFAULT     f   ALTER TABLE ONLY public.review ALTER COLUMN id SET DEFAULT nextval('public.review_id_seq'::regclass);
 8   ALTER TABLE public.review ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    230    231    231            �           2604    16506    reviewtype id    DEFAULT     n   ALTER TABLE ONLY public.reviewtype ALTER COLUMN id SET DEFAULT nextval('public.reviewtype_id_seq'::regclass);
 <   ALTER TABLE public.reviewtype ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    229    228    229            �           2604    16456    users id    DEFAULT     d   ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);
 7   ALTER TABLE public.users ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    223    222    223            �          0    16550    allergie 
   TABLE DATA           8   COPY public.allergie (userid, ingredientid) FROM stdin;
    public          postgres    false    235   �t                 0    16466    blog 
   TABLE DATA           *   COPY public.blog (id, userid) FROM stdin;
    public          postgres    false    225   �t       �          0    16487    blogcategory 
   TABLE DATA           :   COPY public.blogcategory (categoryid, blogid) FROM stdin;
    public          postgres    false    227   �t       �          0    16477    blogcontent 
   TABLE DATA           @   COPY public.blogcontent (headline, content, blogid) FROM stdin;
    public          postgres    false    226   u       y          0    16416    category 
   TABLE DATA           ,   COPY public.category (id, name) FROM stdin;
    public          postgres    false    219   �u       �          0    16565    dailybodyweight 
   TABLE DATA           ?   COPY public.dailybodyweight (userid, weight, date) FROM stdin;
    public          postgres    false    236   3v       �          0    16539 	   dailymenu 
   TABLE DATA           5   COPY public.dailymenu (id, userid, date) FROM stdin;
    public          postgres    false    234   �v       w          0    16409 
   ingredient 
   TABLE DATA           M   COPY public.ingredient (id, carbohydrates, fats, proteins, name) FROM stdin;
    public          postgres    false    217   �v       z          0    16422    ingredientslist 
   TABLE DATA           A   COPY public.ingredientslist (recipeid, ingredientid) FROM stdin;
    public          postgres    false    220   �w       u          0    16400    recipe 
   TABLE DATA           A   COPY public.recipe (id, cookingtime, servings, kcal) FROM stdin;
    public          postgres    false    215   �w       {          0    16437    recipecategory 
   TABLE DATA           >   COPY public.recipecategory (categoryid, recipeid) FROM stdin;
    public          postgres    false    221   &x       �          0    16590 
   recipeinfo 
   TABLE DATA           E   COPY public.recipeinfo (instruction, headline, recipeid) FROM stdin;
    public          postgres    false    238   Ux       �          0    16575 
   recipelist 
   TABLE DATA           ;   COPY public.recipelist (dailymenuid, recipeid) FROM stdin;
    public          postgres    false    237   Dz       �          0    16510    review 
   TABLE DATA           M   COPY public.review (id, score, userid, reviewtypeid, reviewdate) FROM stdin;
    public          postgres    false    231   }z       �          0    16528    reviewcontent 
   TABLE DATA           D   COPY public.reviewcontent (headline, content, reviewid) FROM stdin;
    public          postgres    false    232   �z       �          0    16503 
   reviewtype 
   TABLE DATA           8   COPY public.reviewtype (id, name, targetid) FROM stdin;
    public          postgres    false    229   9{       }          0    16453    users 
   TABLE DATA           >   COPY public.users (id, email, phonenumber, login) FROM stdin;
    public          postgres    false    223   j{       �           0    0    blog_id_seq    SEQUENCE SET     9   SELECT pg_catalog.setval('public.blog_id_seq', 3, true);
          public          postgres    false    224            �           0    0    categories_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.categories_id_seq', 11, true);
          public          postgres    false    218            �           0    0    dailymenu_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.dailymenu_id_seq', 10, true);
          public          postgres    false    233            �           0    0    ingredients_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.ingredients_id_seq', 23, true);
          public          postgres    false    216            �           0    0    recipe_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.recipe_id_seq', 19, true);
          public          postgres    false    214            �           0    0    review_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.review_id_seq', 8, true);
          public          postgres    false    230            �           0    0    reviewtype_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.reviewtype_id_seq', 2, true);
          public          postgres    false    228            �           0    0    users_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.users_id_seq', 7, true);
          public          postgres    false    222            �           2606    16554    allergie allergie_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.allergie
    ADD CONSTRAINT allergie_pkey PRIMARY KEY (userid, ingredientid);
 @   ALTER TABLE ONLY public.allergie DROP CONSTRAINT allergie_pkey;
       public            postgres    false    235    235            �           2606    16471    blog blog_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY public.blog
    ADD CONSTRAINT blog_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.blog DROP CONSTRAINT blog_pkey;
       public            postgres    false    225            �           2606    16491    blogcategory blogcategory_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public.blogcategory
    ADD CONSTRAINT blogcategory_pkey PRIMARY KEY (categoryid, blogid);
 H   ALTER TABLE ONLY public.blogcategory DROP CONSTRAINT blogcategory_pkey;
       public            postgres    false    227    227            �           2606    16421    category categories_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.category
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.category DROP CONSTRAINT categories_pkey;
       public            postgres    false    219            �           2606    16569 $   dailybodyweight dailybodyweight_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public.dailybodyweight
    ADD CONSTRAINT dailybodyweight_pkey PRIMARY KEY (userid, date);
 N   ALTER TABLE ONLY public.dailybodyweight DROP CONSTRAINT dailybodyweight_pkey;
       public            postgres    false    236    236            �           2606    16544    dailymenu dailymenu_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.dailymenu
    ADD CONSTRAINT dailymenu_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.dailymenu DROP CONSTRAINT dailymenu_pkey;
       public            postgres    false    234            �           2606    16414    ingredient ingredients_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.ingredient
    ADD CONSTRAINT ingredients_pkey PRIMARY KEY (id);
 E   ALTER TABLE ONLY public.ingredient DROP CONSTRAINT ingredients_pkey;
       public            postgres    false    217            �           2606    16426 $   ingredientslist ingredientslist_pkey 
   CONSTRAINT     v   ALTER TABLE ONLY public.ingredientslist
    ADD CONSTRAINT ingredientslist_pkey PRIMARY KEY (recipeid, ingredientid);
 N   ALTER TABLE ONLY public.ingredientslist DROP CONSTRAINT ingredientslist_pkey;
       public            postgres    false    220    220            �           2606    16407    recipe recipe_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.recipe
    ADD CONSTRAINT recipe_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.recipe DROP CONSTRAINT recipe_pkey;
       public            postgres    false    215            �           2606    16441 "   recipecategory recipecategory_pkey 
   CONSTRAINT     r   ALTER TABLE ONLY public.recipecategory
    ADD CONSTRAINT recipecategory_pkey PRIMARY KEY (categoryid, recipeid);
 L   ALTER TABLE ONLY public.recipecategory DROP CONSTRAINT recipecategory_pkey;
       public            postgres    false    221    221            �           2606    16579    recipelist recipelist_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public.recipelist
    ADD CONSTRAINT recipelist_pkey PRIMARY KEY (dailymenuid, recipeid);
 D   ALTER TABLE ONLY public.recipelist DROP CONSTRAINT recipelist_pkey;
       public            postgres    false    237    237            �           2606    16517    review review_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.review
    ADD CONSTRAINT review_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.review DROP CONSTRAINT review_pkey;
       public            postgres    false    231            �           2606    16508    reviewtype reviewtype_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.reviewtype
    ADD CONSTRAINT reviewtype_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.reviewtype DROP CONSTRAINT reviewtype_pkey;
       public            postgres    false    229            �           2606    16462    users users_email_key 
   CONSTRAINT     Q   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);
 ?   ALTER TABLE ONLY public.users DROP CONSTRAINT users_email_key;
       public            postgres    false    223            �           2606    16464    users users_login_key 
   CONSTRAINT     Q   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_login_key UNIQUE (login);
 ?   ALTER TABLE ONLY public.users DROP CONSTRAINT users_login_key;
       public            postgres    false    223            �           2606    16460    users users_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    223            �           2606    16482    blogcontent fk_blog    FK CONSTRAINT     �   ALTER TABLE ONLY public.blogcontent
    ADD CONSTRAINT fk_blog FOREIGN KEY (blogid) REFERENCES public.blog(id) ON DELETE CASCADE;
 =   ALTER TABLE ONLY public.blogcontent DROP CONSTRAINT fk_blog;
       public          postgres    false    226    3269    225            �           2606    16497    blogcategory fk_blog2    FK CONSTRAINT     �   ALTER TABLE ONLY public.blogcategory
    ADD CONSTRAINT fk_blog2 FOREIGN KEY (blogid) REFERENCES public.blog(id) ON DELETE CASCADE;
 ?   ALTER TABLE ONLY public.blogcategory DROP CONSTRAINT fk_blog2;
       public          postgres    false    3269    227    225            �           2606    16442    recipecategory fk_category    FK CONSTRAINT     �   ALTER TABLE ONLY public.recipecategory
    ADD CONSTRAINT fk_category FOREIGN KEY (categoryid) REFERENCES public.category(id) ON DELETE CASCADE;
 D   ALTER TABLE ONLY public.recipecategory DROP CONSTRAINT fk_category;
       public          postgres    false    221    219    3257            �           2606    16492    blogcategory fk_category2    FK CONSTRAINT     �   ALTER TABLE ONLY public.blogcategory
    ADD CONSTRAINT fk_category2 FOREIGN KEY (categoryid) REFERENCES public.category(id) ON DELETE CASCADE;
 C   ALTER TABLE ONLY public.blogcategory DROP CONSTRAINT fk_category2;
       public          postgres    false    3257    219    227            �           2606    16580    recipelist fk_dailymenu    FK CONSTRAINT     �   ALTER TABLE ONLY public.recipelist
    ADD CONSTRAINT fk_dailymenu FOREIGN KEY (dailymenuid) REFERENCES public.dailymenu(id) ON DELETE CASCADE;
 A   ALTER TABLE ONLY public.recipelist DROP CONSTRAINT fk_dailymenu;
       public          postgres    false    3277    237    234            �           2606    16432    ingredientslist fk_ingredient    FK CONSTRAINT     �   ALTER TABLE ONLY public.ingredientslist
    ADD CONSTRAINT fk_ingredient FOREIGN KEY (ingredientid) REFERENCES public.ingredient(id) ON DELETE CASCADE;
 G   ALTER TABLE ONLY public.ingredientslist DROP CONSTRAINT fk_ingredient;
       public          postgres    false    3255    220    217            �           2606    16560    allergie fk_ingredient2    FK CONSTRAINT     �   ALTER TABLE ONLY public.allergie
    ADD CONSTRAINT fk_ingredient2 FOREIGN KEY (ingredientid) REFERENCES public.ingredient(id) ON DELETE CASCADE;
 A   ALTER TABLE ONLY public.allergie DROP CONSTRAINT fk_ingredient2;
       public          postgres    false    217    235    3255            �           2606    16427    ingredientslist fk_recipe    FK CONSTRAINT     �   ALTER TABLE ONLY public.ingredientslist
    ADD CONSTRAINT fk_recipe FOREIGN KEY (recipeid) REFERENCES public.recipe(id) ON DELETE CASCADE;
 C   ALTER TABLE ONLY public.ingredientslist DROP CONSTRAINT fk_recipe;
       public          postgres    false    220    3253    215            �           2606    16447    recipecategory fk_recipe2    FK CONSTRAINT     �   ALTER TABLE ONLY public.recipecategory
    ADD CONSTRAINT fk_recipe2 FOREIGN KEY (recipeid) REFERENCES public.recipe(id) ON DELETE CASCADE;
 C   ALTER TABLE ONLY public.recipecategory DROP CONSTRAINT fk_recipe2;
       public          postgres    false    3253    221    215            �           2606    16585    recipelist fk_recipe3    FK CONSTRAINT     �   ALTER TABLE ONLY public.recipelist
    ADD CONSTRAINT fk_recipe3 FOREIGN KEY (recipeid) REFERENCES public.recipe(id) ON DELETE CASCADE;
 ?   ALTER TABLE ONLY public.recipelist DROP CONSTRAINT fk_recipe3;
       public          postgres    false    3253    237    215            �           2606    16595    recipeinfo fk_recipe3    FK CONSTRAINT     �   ALTER TABLE ONLY public.recipeinfo
    ADD CONSTRAINT fk_recipe3 FOREIGN KEY (recipeid) REFERENCES public.recipe(id) ON DELETE CASCADE;
 ?   ALTER TABLE ONLY public.recipeinfo DROP CONSTRAINT fk_recipe3;
       public          postgres    false    3253    238    215            �           2606    16533    reviewcontent fk_review    FK CONSTRAINT     �   ALTER TABLE ONLY public.reviewcontent
    ADD CONSTRAINT fk_review FOREIGN KEY (reviewid) REFERENCES public.review(id) ON DELETE CASCADE;
 A   ALTER TABLE ONLY public.reviewcontent DROP CONSTRAINT fk_review;
       public          postgres    false    3275    232    231            �           2606    16523    review fk_reviewtype    FK CONSTRAINT     �   ALTER TABLE ONLY public.review
    ADD CONSTRAINT fk_reviewtype FOREIGN KEY (reviewtypeid) REFERENCES public.reviewtype(id) ON DELETE CASCADE;
 >   ALTER TABLE ONLY public.review DROP CONSTRAINT fk_reviewtype;
       public          postgres    false    3273    229    231            �           2606    16472    blog fk_user    FK CONSTRAINT     |   ALTER TABLE ONLY public.blog
    ADD CONSTRAINT fk_user FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE;
 6   ALTER TABLE ONLY public.blog DROP CONSTRAINT fk_user;
       public          postgres    false    3267    225    223            �           2606    16518    review fk_user2    FK CONSTRAINT        ALTER TABLE ONLY public.review
    ADD CONSTRAINT fk_user2 FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE;
 9   ALTER TABLE ONLY public.review DROP CONSTRAINT fk_user2;
       public          postgres    false    223    231    3267            �           2606    16545    dailymenu fk_user3    FK CONSTRAINT     �   ALTER TABLE ONLY public.dailymenu
    ADD CONSTRAINT fk_user3 FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE;
 <   ALTER TABLE ONLY public.dailymenu DROP CONSTRAINT fk_user3;
       public          postgres    false    234    223    3267            �           2606    16555    allergie fk_user4    FK CONSTRAINT     �   ALTER TABLE ONLY public.allergie
    ADD CONSTRAINT fk_user4 FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE;
 ;   ALTER TABLE ONLY public.allergie DROP CONSTRAINT fk_user4;
       public          postgres    false    223    235    3267            �           2606    16570    dailybodyweight fk_user5    FK CONSTRAINT     �   ALTER TABLE ONLY public.dailybodyweight
    ADD CONSTRAINT fk_user5 FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE;
 B   ALTER TABLE ONLY public.dailybodyweight DROP CONSTRAINT fk_user5;
       public          postgres    false    223    236    3267            �      x�3�4�2�4�2�4�2�4bS�=... '�            x�3�4�2�4�2�4����� A      �      x�3�4�2�4�2�4����� A      �   �   x�]��nAE띯�]�d$���tH44f���c�@���� �RY�u�����&+�\��5B���vHrc�i�ٿ�{ғdR��sf�ز�[�yz���ne6���x���Bz���g��I��K-h>}o�GF���f��A���2�&|)�B��n?C/ĥ�����"<�t�}x>��
,�X��Os�Ƙ�eS      y   L   x�3�H,.I�2��MM,�2�N�IL�2�tMO/�2�t��L�N��2�t.-*��2��LN��t+*�,����� 7'      �   A   x�M̻�0�Z��E����pRY����}��l썁��ndsxT���U��]��� 	�K      �   /   x�3�4�4202�50�50�2B�qs!˚ s��L9��ec���� 0�      w   �   x�5O�j�0<�~���he+�٦���c/"��"�Ud)��U�2�,��c[6`�ӷ'�9�Ńhz������HX7��O��~i�IdrpƠN�"_��5��%��VE�ؗuJ1δS�*gq�%5C?S-b�)���s$�rj�&����L�'�����m����/9K"�jWkQ�(�ه�J����0��E�v�i0��~�c�����7�C�      z   +   x�3�4�2�4bs ��2�44 � ˄����b���� �0�      u   3   x���  ���3
�E���A?�I�ٔ�E�(+	��f�h�!��u      {      x�3�4�2�4�2�4�2�4�2�=... (�      �   �  x�}��n�0E��W�D�$���vբ�vCQ#����!��Q��h�r��3��P&�ڀv ��0!�E�t}� �-̤p�e@�Z-H��_ �D%��W����*�N{;�d0ZuU�Cx��_��{?9����NsGR�]N`�Yj�+N�do0��Q[KeG���d$�(�Y��k����t+}�~�C��G񒓚����Ñ�B�8RN�Ɛ��n��J�h0�־+�}��4!�#Ho0�ՍD�V&��V�(#ٹ�R�IB!z�S���!+,��C
f�5�y�4�$�#۝��#�9�K�Ȣ���j
���EN�?+Dx~��V��ܴ;�1�G#�!֖ئ���,�x7����{��QN���!�A����ʶ�@�m~���z$筗�����|-/�u��:.T�Z�	D�Ŵ:2��6���4�:\�r�o���1�8H�ɡ蒏�G����jZE4D�5 .pfq���m���Ra      �   )   x�3�4�2�4b.cNS 6�2bS��)�o
����� hT�      �   H   x�U��	�0Cѳ���-'i�K��#n���o�@CTr�<-:��vL�N�"�}ts@����:_���B|�2s�      �   T   x���WH�I�K��TH�L�S/Q(JM�ɩT���NU(�HUp/JM�V(N�IL�%g��)x�(�'+��t�q�q�w�b���� cBe      �   !   x�3�JM�,H�4�2�t��O�4����� QF�      }   r   x�]�;�0k�.�?��r�4��@�8>��Ʃ��Ѿ��{�����c��o��< ���4I�t���CD��i�J�25G�	�jW����F����OS��.sIPgxy�i��D� �R8�     