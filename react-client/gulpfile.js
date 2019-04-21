"use strict";

var gulp = require("gulp");
var sass = require("gulp-sass");

sass.compiler = require("node-sass");

//compile
gulp.task("sass", function() {
  return gulp
    .src("./src/assets/scss/**/*.scss")
    .pipe(sass().on("error", sass.logError))
    .pipe(gulp.dest("./public/css"));
});

//compile and watch

gulp.task("sass:watch", function() {
  gulp.watch(
    ["src/assets/scss/*.scss", "src/assets/scss/**/*.scss"],
    function() {
      return gulp
        .src("./src/assets/scss/**/*.scss")
        .pipe(sass().on("error", sass.logError))
        .pipe(gulp.dest("./public/css"));
    }
  );
});
