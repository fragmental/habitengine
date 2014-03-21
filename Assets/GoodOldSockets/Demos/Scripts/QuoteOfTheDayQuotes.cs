using UnityEngine;

namespace LostPolygon.Examples {
    public partial class QuoteOfTheDay {

        private static readonly string[] Quotes = new[] {
            @"Every beauty which is seen here below by persons
of perception resemble more than anything else that celestial
source from which we all are come....
-Michelangelo (1475-1564)",

            @"I celebrate myself, and sing myself,
And what I assume you shall assume,
For every atom belonging to me as good belongs to you.
-Walt Whitman (1819-1892)",

            @"Heaven will be inherited by every man
who has heaven in his soul.
-Henry Ward Beecher (1813-1887)",

            @"Imagination is more important than knowledge.
Knowledge is limited. Imagination encircles the world.
-Albert Einstein (1879-1955)",

            @"Learn to get in touch with the silence within yourself
and know that everything in life has a purpose.
-Elisabeth Kübler-Ross (1926 -)",

            @"Beauty in things exists in the mind which contemplates them.
-David Hume (1711-1776)",

            @"The whole of life, from the moment you are born
to the moment you die, is a process of learning.
-Jiddu Krishnamurti (1895-1986)",

            @"The mystic bond of brotherhood makes all men one.
-Thomas Carlyle (1795-1881)",

            @"My heart leaps up when I behold a rainbow in the sky.
-William Wordsworth (1770-1850)",

            @"The clearest way into the universe
is through a forest wilderness.
-John Muir (1838-1914)",

            @"What else is nature but God?
-Seneca (4? B.C.-65 A.D.)",

            @"A faithful friend is the medicine of life.
-The Apocrypha, 6:16",

            @"They are alive and well somewhere,
the smallest sprout shows there is really no death...
-George Washington Carver (1864-1943)",

            @"The superior reasoning power...revealed in the
incomprehensible universe, forms my idea of God.
-Albert Einstein (1879-1955)",

            @"Patience is the companion of wisdom.
-Saint Augustine (354-430)",

            @"Most folks are as happy as they make up their minds to be.
-Abraham Lincoln (1809-1865)",

            @"In my Father's house are many mansions.
-The Bible, John 14:2",

            @"Sit in reverie and watch the changing color of the waves
that break upon the idle seashore of the mind.
-Henry Wadsworth Longfellow (1807-1882)",

            @"People are like stained-glass windows.
They sparkle and shine when the sun is out,
but when the darkness sets in,
their true beauty is revealed only
if there is light from within.
-Elisabeth Kübler-Ross (1926 -)",

            @"Life is what we make it, always has been, always will be.
-Grandma Moses (1860-1961)",

            @"The best and most beautiful things in the world
cannot be seen, nor touched...but are felt in the heart.
-Helen Keller (1880-1968)",

            @"Fourscore and seven years ago our fathers
brought forth on this continent a new nation,
conceived in liberty and dedicated to the proposition
that all men are created equal.
-Abraham Lincoln (1809-1865)",

            @"Because man and woman are the complement of one another,
we need woman's thought in national affairs to make
a safe and stable government.
-Elizabeth Cady Stanton (1815-1902)",

            @"You are the people who are shaping a better world.
One of the secrets of inner peace is the practice of compassion.
-Dalai Lama (1935 -)",

            @"It is a wholesome and necessary thing for us to turn
again to the earth and in the contemplation of her beauties
to know of wonder and humility.
-Rachel Carson (1907-1964)",

            @"Those things that nature denied to human sight,
she revealed to the eyes of the soul.
-Ovid (43 B.C.-17 A.D.)",

            @"Change your thoughts, and you change your world.
-Norman Vincent Peale (1898-1993)",

            @"To me every hour of the light and dark is a miracle,
Every cubic inch of space is a miracle.
-Walt Whitman (1819-1892)",

            @"For what is Mysticism? It is not the attempt to draw near to God,
not by rites or ceremonies, but by inward disposition?
Is it not merely a hard word for 'The Kingdom of Heaven is within'?
Heaven is neither a place nor a time.
-Florence Nightingale (1820-1910)",

            @"Those who deny freedom to others deserve it not for themselves,
and, under a just God, cannot retain it.
-Abraham Lincoln (1809-1865)",

            @"We all live with the objective of being happy;
our lives are all different and yet the same.
-Anne Frank (1929-1945)",

            @"Believe nothing, no matter where you read it, or who said it,
no matter if I have said it, unless it agrees with your own
reason and your own common sense.
-Buddha (536 B.C.-483 B.C.)",

            @"Sometime they'll give a war and nobody will come.
-Carl Sandburg (1878-1967)",

            @"If the first woman God ever made was strong enough to turn
the world upside down all alone, these women together ought
to be able to turn it back, and get it right side up again!
And now they is asking to do it, the men better let them.
-Sojourner Truth (1797-1883)",

            @"The true republic: men, their rights and nothing more:
women, their rights and nothing less.
-Susan B. Anthony (1820-1906)",

            @"The soul should always stand ajar.
Ready to welcome the ecstatic experience.
-Emily Dickinson (1830-1886)",

            @"Where'er a noble deed is wrought,
Where'er is spoken a noble thought,
Our hearts in glad surprise
To higher levels rise.
-Henry Wadsworth Longfellow (1807-1882)",

            @"Intuition will tell the thinking mind where to look next.
-Jonas Salk (1914-1995)",

            @"Live Large!
Don Pendleton (1927-1995)",

            @"So many gods, so many creeds;
So many paths that wind and wind,
While just the art of being kind
Is all the sad world needs.
-Ella Wheeler Wilcox (1850-1919)",

            @"You cannot play the game of life with sweaty palms.
-Dr. Phil, Phillip C. McGraw (1950 -)",

            @"If something comes to life in others because of you,
then you have made an approach to immortality.
-Norman Cousins (1912-1990)",

            @"Surround yourself with only people who are going to lift you higher.
-Oprah Winfrey (1954 -)",

            @"Freedom is the last, best hope of earth.
-Abraham Lincoln (1809-1865)",

            @"Woman will always be dependant until she holds a purse of her own.
-Elizabeth Cady Stanton (1815-1902)",

            @"Follow your bliss.
-Joseph Campbell (1904-1987)",

            @"What befalls the earth befalls all the sons of the earth.
This we know: the earth does not belong to man, man belongs to the earth.
All things are connected like the blood that unites us all.
Man does not weave this web of life. He is merely a strand of it.
Whatever he does to the web, he does to himself.
-Chief Seattle (1786-1866)",

            @"I found that when I talk to the little flower or to the little peanut
they will give up their secrets...
-George Washington Carver (1804-1903)",

            @"Science may have found a cure for most evils,
but it has found no remedy for the worst of them all-
the apathy of human beings.
-Helen Keller (1880-1968)",

            @"Follow your instincts. That's where true wisdom manifests itself.
-Oprah Winfrey (1954 -)",

            @"Time is
Too slow for those who Wait,
Too swift for those who Fear,
Too long for those who Grieve,
Too short for those who Rejoice,
But for those who Love
Time is not.
-Henry Vandyke (1852-1933)",

            @"God is the friend of silence.
See how nature-trees, flowers, grass-grows in silence;
see the stars, the moon and the sun, how they move in silence...
We need silence to be able to touch souls.
-Mother Teresa (1910-1997)",

            @"When one door of happiness closes, another opens;
but often we look so long at the closed door that we do
not see the one which has been opened for us.
-Helen Keller (1880-1968)",

            @"Hope is a thing with feathers
That perches in the soul;
And sings the tune without words
And never stops at all.
-Emily Dickinson (1830-1886)",

            @"I could not, at any age, be content to take my place
by the fireside and simply look on. Life was meant to be lived.
Curiosity must be kept alive. One must never, for whatever reason,
turn his back on life.
-Eleanor Roosevelt (1884-1962)",

            @"Peace cannot be kept by force.
It can only be achieved by understanding.
-Albert Einstein (1879-1955)",

            @"Leave nothing for tomorrow which can be done today.
-Abraham Lincoln (1809-1865)",

            @"Meditation is not a means to an end.
It is both the means and the end.
-Jiddu Krishnamurti (1895-1986)",

            @"Some people are so afraid to die that they never begin to live.
-Henry Vandyke (1852-1933)",

            @"Faith furnishes prayer with wings,
without which it cannot soar to Heaven.
-St. John Climacus (525-600)",

            @"No one can make you feel inferior without your consent.
-Eleanor Roosevelt (1884-1962)",

            @"It is my conviction that it is the intuitive, spiritual aspects
of us humans-the inner voice-that gives us the 'knowing,'
the peace, and the direction to go through the windstorms of life,
not shattered but whole, joining in love and understanding.
-Elisabeth Kübler-Ross (1926 -)",

            @"We are not human beings having a spiritual experience.
We are spiritual beings having a human experience.
-Pierre Teilhard de Chardin (1881-1955)",

            @"You give but little when you give of your possessions.
It is when you give of yourself that you truly give.
-Kahlil Gibran (1883-1931)",

            @"Just as there is no loss of basic energy in the universe,
so no thought or action is without its effects,
present or ultimate, seen or unseen, felt or unfelt.
-Norman Cousins (1912-1990)",

            @"It is better to be a lion for a day than a sheep all your life.
-Sister Elizabeth Kenny (1886-1952)",

            @"Nature is an unlimited broadcasting station, through which God
speaks to us every hour, if we only will tune in.
-George Washington Carver (1864-1943)",

            @"The future belongs to those who believe in the beauty of their dreams.
-Eleanor Roosevelt (1884-1962)",

            @"Our birth is but a sleep and a forgetting;
The soul that rises with us, our life's star,
Hath had elsewhere its setting,
And cometh from afar;
Not in entire forgetfulness,
And not in utter nakedness,
But railing clouds of glory do we come
From God, who is our home.
-William Wordsworth (1770-1850)",

            @"When you see your brother, you see God.
-St. Clement of Alexandria (c.A.D. 150-c.215)",

            @"As a tale, so is life; not how long
it is, but how good it is, is what matters.
-Seneca (4? B.C.-65 A.D.)",

            @"God will not look your over for medals,
degrees or diplomas, but for scars.
-Elbert Hubbard (1856-1915)",

            @"The soul is not where it lives but where it loves.
-Thomas Fuller (1654-1734)",

            @"There is another reality enfolding ours-as close as our breath!
-Don Pendleton (1927-1995)",

            @"Hush, my dear, lie still and slumber
Holy Angels guard thy bed!
Heavenly blessings without number
Gently falling on thy head.
-Isaac Watts (1674-1748)",

            @"I long to accomplish a great and noble task, but it is my chief
duty to accomplish small tasks as if they were great and noble.
-Helen Keller (1880-1968)",

            @"Love is doing small things with great love.
-Mother Teresa (1910-1997)",

            @"Conscience is God's presence in man.
-Emanuel Swedenborg (1688-1772)",

            @"Love is the only thing that we can carry with us when we go,
and it makes the end so easy.
-Louisa May Alcott (1832-1888)",

            @"The veil that clouds your eyes shall
be lifted by the hands that wove it.
-Kahlil Gibran (1883-1931)",

            @"The universe is but one vast Symbol of God.
-Thomas Carlyle (1795-1881)",

            @"All the kindness which a man puts out into the world
works on the heart and thoughts of mankind.
-Albert Schweitzer (1875-1965)",

            @"I leave you, hoping that the lamp of liberty
will burn in your bosoms until there shall no longer
be a doubt that all men are created free and equal.
-Abraham Lincoln (1809-1865)",
        };
    }
}
