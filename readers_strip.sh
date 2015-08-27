#!/bin/bash
for file in *.cs; do
  perl -0777 -pi -e 's/(\/\/.*\n)|(namespace(.|\n)*\{)|(namespace(.|\n)*\{)|(\ \ )|(existingInstance\.)|((= |)input\.Read)|(\(.*\);)|(\ *return.*;)|(\ *\})|(if(.|\n)*new\ .*\);)//g' $file
  perl -0777 -pi -e 's/^\s*\n//gm' $file
  mv "$file" "`basename $file .cs`.txt"
done
