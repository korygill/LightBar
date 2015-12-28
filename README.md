# LightBar #

Just some sample code to demonstrate a WPF app that uses `Task.Run` to update UI using `SynchronizationContext` to simulate workload.

I wrote this for fun to see how WPF might perform over the System.Drawing classes.

The overhead does not seem that bad at 20 updates per second, which is probably way more than what a UI should update a control anyway. I would use a moving average of samples, and update like 3-4 times a seconds.

I did this in response to a question at stackoverflow [what-is-the-best-way-to-draw-layers-on-a-control-in-c#](http://stackoverflow.com/questions/34487251/what-is-the-best-way-to-draw-layers-on-a-control-in-c).

To finish this off, I would re-style the thumb on the slider and make the track transparent.

## Design Mode ##

![Design Mode](http://i.imgur.com/EaYZOcb.png)

## When Running ##

![When Running](http://i.imgur.com/0NrNwgg.png)

