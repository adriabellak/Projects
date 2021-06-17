#|
10/03/2021
Ricardo Alonso Aróstegui A01029011
Agustín Pumarejo Ontañón A01028997
Adriana Abella Kuri A01329591
|#

#lang racket

(require racket/trace)

(define (validate-line line dfa init-state init-previous-state)
  (let loop
    ([lst (regexp-split #px"(?!\\#?\\w)" line)];([lst (regexp-split #px"\\b" line)]
    [state init-state] ; The init-state parameter
    [previous-state init-previous-state]
    [token-list empty]
    [transition (car dfa)] ; The first element in the list
    ) 
    (if (empty? lst)
      ; Check if the final state is in the list of acceptables
      (if (member state (caddr dfa))
        ; Return the list of tokens and the last accept state
        (values token-list state previous-state)
        #f)
      (let-values
        ([(state token-type previous-state) (transition state (car lst) previous-state)])
        ; Recursive call
        (loop
          (cdr lst)
          state
          previous-state
          ; Add valid tokens to the list
          (if token-type
            (append token-list (list (list token-type (car lst) state previous-state)))
            token-list)
          ; Pass the same function again
          transition)))))



(define (validate-document init-list dfa)
  (let loop
    ([lst init-list]
    [result empty]
    [state (cadr dfa)]
    [previous-state #f])
    (if (empty? lst)
      result
      (let-values
        ([(token-list state previous-state) (validate-line (car lst) dfa state previous-state)])
        (loop
          (cdr lst)
          (append result (list token-list))
          state
          previous-state)))))



(define (css-tokens-validation state string previous-state)
  (cond
    [(eq? state 'selector) (cond
      [(regexp-match? #rx".*\\/\\*" string) (values 'comment 'b 'selector)]
      [(regexp-match? #rx".*\\/" string) (values 'isComment 'b 'selector)]
      [(regexp-match? #rx"^\\-?[a-zA-Z_]\\w*" string) (values 'selector 'element #f)]
      [(regexp-match? #rx"^\\.\\-?[a-zA-Z_]\\w*" string) (values 'selector 'class #f)]
      [(regexp-match? #rx"^\\#\\-?[a-zA-Z_]\\w*" string) (values 'selector 'id #f)]
      [(regexp-match? #rx"\\s*\\{" string) (values 'property 'b #f)]
      [else (values 'selector 'b #f)])]
    [(eq? state 'property) (cond
      [(regexp-match? #rx".*\\/\\*" string) (values 'comment 'b 'property)]
      [(regexp-match? #rx".*\\/" string) (values 'isComment 'b 'property)]
      [(regexp-match? #rx"\\s*\\}" string) (values 'selector 'b #f)]
      [(regexp-match? #rx"[a-z]+(\\-[a-z]+)*\\s*" string) (values 'property 'property #f)]
      [(regexp-match? #rx"\\s*\\:" string) (values 'value 'b #f)]
      [else (values 'property 'b #f)])]
    [(eq? state 'value) (cond
      [(regexp-match? #rx".*\\/\\*" string) (values 'comment 'b 'value)]
      [(regexp-match? #rx".*\\/" string) (values 'isComment 'b 'value)]
      [(regexp-match? #rx"\\s*\\}" string) (values 'selector 'b #f)]
      [(regexp-match? #rx"[a-z0-9]+(\\s+[a-z0-9]+)*\\s*" string) (values 'value 'value #f)]
      [(regexp-match? #rx"\\s*\\;" string) (values 'property 'b #f)]
      [else (values 'value 'b #f)])]
    [(eq? state 'isComment) (cond
      [(regexp-match? #rx"\\*" string) (values 'comment 'b previous-state)]
      [else (values previous-state 'b #f)])]
    [(eq? state 'comment) (cond
      [(regexp-match? #rx".*\\*" string) (values 'isClosingComment 'b previous-state)]
      [else (values 'comment 'b previous-state)])]
    [(eq? state 'isClosingComment) (cond
      [(regexp-match? #rx"\\/" string) (values previous-state 'b #f)]
      [else (values 'comment 'b previous-state)])]
    [else (values 'b 'b #f)]))



(define (element-to-html element)
  (cond
    [(eq? (car element) 'element) (string-append "<span class='element'>" (cadr element))]
    [(eq? (car element) 'class) (string-append "<span class='class'>" (cadr element))]
    [(eq? (car element) 'id) (string-append "<span class='id'>" (cadr element))]
    [(eq? (car element) 'property) (string-append "<span class='property'>" (cadr element))]
    [(eq? (car element) 'value) (string-append "<span class='value'>" (cadr element))]
    [(eq? (car element) 'b) (string-append "<span>" (cadr element))]
    [else element]))



(define (line-tokens-to-html init-line)
  (let loop
    ([lst init-line]
    [result "<p>"])
    (if (empty? lst)
      (string-append result "</p>")
      (let-values
          ([(element) (car lst)])
          (loop
            (cdr lst)
            (string-append result (string-append (element-to-html element) "</span>")))))))



(define (document-tokens-to-html in-file-path)
  (let loop
    ([lst (validate-document (file->lines in-file-path) (list css-tokens-validation 'selector (list 'selector 'property 'value 'b 'isComment 'comment 'isClosingComment)))]
    [result '("<html><head><link type='text/css' rel='stylesheet' href='./style.css'></head><body>")])
    (if (empty? lst)
      (append result (list "</body></html>"))
      (loop
        (cdr lst)
        (append result (list (line-tokens-to-html (car lst))))))))



; read from a file
(define (file-to-html in-file-path)
  (display-lines-to-file
    (document-tokens-to-html in-file-path)
    (string-append (car (regexp-match #px"[\\w+\\s*]*" in-file-path)) ".html")
    #:exists 'truncate))



(define (files-to-html files)
  (future (lambda ()
    (map file-to-html files))))



(define (files-with-extension files extension)
  (let loop
    ([lst files]
    [result empty])
    (if (empty? lst)
      result
      (loop
        (cdr lst)
        (if (regexp-match (string-append "\\" extension "$") (car lst))
          (append result (list (car lst)))
          result)))))



(define (split-by lst n)
   (if (not (empty? lst))
       (cons (take lst n) (split-by (drop lst n) n))
       '() ))



(define (directory-CSS-To-HTML num-futures)
  (define futures (map files-to-html (split-by (files-with-extension (map path->string (directory-list)) ".css") (/ (length (files-with-extension (map path->string (directory-list)) ".css")) num-futures))))
  (map touch futures))

; (file-to-tokens "infile.txt" "outfikle.html")
; (map file-to-tokens '("infile.txt" "style.css") '("outfile.html" "style.html"))
; (split-by (map path->string (directory-list)) (/ (length (map path->string (directory-list))) 2))
; (validate-line "/*comment" (list css-tokens-validation 'selector (list 'selector 'property 'value 'b)) 'selector '#f)
; (validate-document '("id{" "comment" "sada") (list css-tokens-validation 'selector (list 'selector 'property 'value 'b)))