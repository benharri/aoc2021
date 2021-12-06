#!/bin/sh

. ./session_cookie.txt

if [ -z "$session" ]; then
  printf "missing session cookie\n"
fi

if [ -z "$1" ]; then
  day=$(date +"%e" | xargs)
else
  day="$1"
fi
  
curl -s https://adventofcode.com/2021/day/"$day"/input \
  --cookie "session=$session" \
  -o "$(printf "input/day%02d.in" "$day")"

class=$(printf "Day%02d.cs" "$day")
longDay=$(printf "%02d" "$day")
if [ ! -f "$class" ]; then
  sed -e "s/Q/$day/g" -e "s/XX/$longDay/g" DayXX.cs.txt > "$class"
fi
