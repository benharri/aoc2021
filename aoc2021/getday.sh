#!/bin/sh

. ./session_cookie.txt

if [ -z "$session" ]; then
  printf "missing session cookie\n"
fi
  
curl -s https://adventofcode.com/2021/day/"$1"/input \
  --cookie "session=$session" \
  -o "$(printf "input/day%02d.in" "$1")"

sed "s/XX/$1/g" DayXX.cs.txt > "$(printf "Day%02d.cs" "$1")"
