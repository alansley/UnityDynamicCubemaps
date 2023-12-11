I want objects to fade out as they get close to the camera - but when we put the `Lit` shader in transparent mode it does NOT write to the depth buffer so you can see geometry you shouldn't be able to through any rendered meshes!

To fix this, I've created this super-simple shader graph where the key difference is that writing to the depth buffer is forced to always - and now everything renders as it should.

See: https://forum.unity.com/threads/problem-with-material-remaining-transparent-even-when-at-full-alpha.1236706/#post-7878076