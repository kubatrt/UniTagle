Unitgle
Prototype project for game like "Untagle".

RESOURCES:
Planar graph - fraw without line crossing
http://blog.ivank.net/game-like-untangle.html
http://www.wyrmtale.com/blog/2013/115/2d-line-intersection-in-c


GAME:
- Generate random 2D points
- Compute triangulation
- throw out some edges to make graph simpler
- shuffle vertices on random positionis (or put them in circle)
- let the user move vertices

PlanarGraph
- container for all the lines
- container for vertices

GraphLine
- connect vertices A with B
- check own intersection

GraphVertice
- hold neighboards

************************************************** 

TODO:
- (9) remove random lines from graph
- (6) better lines adding method
- (7) use XML custom settings for graph
- (8) In-editor graph editor 
- (10) add TestTools plugin
- (12) prepare test for line intersection
- (11) change code editor to VisualStudio 2012

DONE:
- (DONE) end/start points line intersection bug
- (DONE) graph, vertice, line objects
- (DONE) move vertices by mouse
- (DONE) line rendering between
- (DONE) generate vertices around circle
- (DONE) line intersection