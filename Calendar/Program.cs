using System;
using System.Collections.Generic;

class Event
{
    public string Title { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Description { get; set; }

    public Event(string title, DateTime startTime, DateTime endTime, string description)
    {
        Title = title;
        StartTime = startTime;
        EndTime = endTime;
        Description = description;
    }

    public string GetDetails()
    {
        return $"Title: {Title}\nStart: {StartTime}\nEnd: {EndTime}\nDescription: {Description}\n";
    }
}

class CalendarManager
{
    private Dictionary<DateTime, List<Event>> eventDictionary;

    public CalendarManager()
    {
        eventDictionary = new Dictionary<DateTime, List<Event>>();
    }

    public void AddEvent(Event newEvent)
    {
        var eventDate = newEvent.StartTime.Date;
        if (!eventDictionary.ContainsKey(eventDate))
        {   
            eventDictionary[eventDate] = new List<Event>();
        }
        eventDictionary[eventDate].Add(newEvent);
        Console.WriteLine("Event successfully added.");
    }

    public void EditEvent(string title, Event updatedEvent)
    {
        foreach (var date in eventDictionary.Keys)
        {
            var eventList = eventDictionary[date];
            var existingEvent = eventList.Find(e => e.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (existingEvent != null)
            {
                existingEvent.Title = updatedEvent.Title;
                existingEvent.StartTime = updatedEvent.StartTime;
                existingEvent.EndTime = updatedEvent.EndTime;
                existingEvent.Description = updatedEvent.Description;
                Console.WriteLine("Event updated successfully.");
                return;
            }
        }
        Console.WriteLine("Event not found.");
    }

    public void DeleteEvent(string title)
    {
        foreach (var date in eventDictionary.Keys)
        {
            var eventList = eventDictionary[date];
            var eventToRemove = eventList.Find(e => e.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (eventToRemove != null)
            {
                eventList.Remove(eventToRemove);
                Console.WriteLine("Event deleted successfully.");
                return;
            }
        }
        Console.WriteLine("Event not found.");
    }

    public void ViewEventsOnDate(DateTime date)
    {
        var eventDate = date.Date;
        if (eventDictionary.ContainsKey(eventDate))
        {
            Console.WriteLine($"Events on {eventDate.ToShortDateString()}:");
            foreach (var ev in eventDictionary[eventDate])
            {   
                Console.WriteLine("-----------------");
                Console.WriteLine(ev.GetDetails());
            }
        }
        else
        {
            Console.WriteLine("No events found on this date.");
        }
    }

    public void ViewEventsByWeek(DateTime date)
    {
        DateTime startOfWeek = date.Date.AddDays(-(int)date.DayOfWeek);
        DateTime endOfWeek = startOfWeek.AddDays(7);

        Console.WriteLine($"Events for the week starting {startOfWeek.ToShortDateString()}:");
        foreach (var day in eventDictionary.Keys)
        {
            if (day >= startOfWeek && day < endOfWeek)
            {
                foreach (var ev in eventDictionary[day])
                {   
                    Console.WriteLine("-----------------");
                    Console.WriteLine(ev.GetDetails());
                }
            }
        }
    }

    public void ViewEventsByMonth(int year, int month)
    {
        Console.WriteLine($"Events for {year}-{month:D2}:");
        foreach (var day in eventDictionary.Keys)
        {
            if (day.Year == year && day.Month == month)
            {
                foreach (var ev in eventDictionary[day])
                {   
                    Console.WriteLine("-----------------");
                    Console.WriteLine(ev.GetDetails());
                }
            }
        }
    }

    public void ViewEventsByYear(int year)
    {
        Console.WriteLine($"Events for the year {year}:");
        foreach (var day in eventDictionary.Keys)
        {
            if (day.Year == year)
            {
                foreach (var ev in eventDictionary[day])
                {   
                    Console.WriteLine("-----------------");
                    Console.WriteLine(ev.GetDetails());
                }
            }
        }
    }

    public void ViewAllEvents()
    {
        if (eventDictionary.Count == 0)
        {   
            Console.WriteLine("-----------------");
            Console.WriteLine("No events available.");
            return;
        }

        Console.WriteLine("All Events:");
        foreach (var date in eventDictionary.Keys)
        {   
            Console.WriteLine("-----------------");
            Console.WriteLine($"Date: {date.ToShortDateString()}");
            foreach (var ev in eventDictionary[date])
            {
                Console.WriteLine(ev.GetDetails());
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        CalendarManager calendar = new CalendarManager();
        while (true)
        {
            Console.WriteLine("\nCalendar Manager");
            Console.WriteLine("1. Add Event");
            Console.WriteLine("2. Edit Event");
            Console.WriteLine("3. Delete Event");
            Console.WriteLine("4. View Events on Specific Date");
            Console.WriteLine("5. View Events for the Week");
            Console.WriteLine("6. View Events for the Month");
            Console.WriteLine("7. View Events for the Year");
            Console.WriteLine("8. View All Events");
            Console.WriteLine("9. Exit");
            Console.Write("Select an option: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter event title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter start date and time (yyyy-MM-dd HH:mm): ");
                        DateTime startTime = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter end date and time (yyyy-MM-dd HH:mm): ");
                        DateTime endTime = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter description: ");
                        string description = Console.ReadLine();

                        calendar.AddEvent(new Event(title, startTime, endTime, description));
                        break;

                    case 2:
                        Console.Write("Enter the title of the event to edit: ");
                        string editTitle = Console.ReadLine();
                        Console.Write("Enter new title: ");
                        string newTitle = Console.ReadLine();
                        Console.Write("Enter new start date and time (yyyy-MM-dd HH:mm): ");
                        DateTime newStartTime = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter new end date and time (yyyy-MM-dd HH:mm): ");
                        DateTime newEndTime = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter new description: ");
                        string newDescription = Console.ReadLine();

                        calendar.EditEvent(editTitle, new Event(newTitle, newStartTime, newEndTime, newDescription));
                        break;

                    case 3:
                        Console.Write("Enter the title of the event to delete: ");
                        string deleteTitle = Console.ReadLine();
                        calendar.DeleteEvent(deleteTitle);
                        break;

                    case 4:
                        Console.Write("Enter the date to view events (yyyy-MM-dd): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());
                        calendar.ViewEventsOnDate(date);
                        break;

                    case 5:
                        Console.Write("Enter a date to view the week (yyyy-MM-dd): ");
                        DateTime weekDate = DateTime.Parse(Console.ReadLine());
                        calendar.ViewEventsByWeek(weekDate);
                        break;

                    case 6:
                        Console.Write("Enter the year and month to view events (yyyy-MM): ");
                        var monthInput = Console.ReadLine().Split('-');
                        int year = int.Parse(monthInput[0]);
                        int month = int.Parse(monthInput[1]);
                        calendar.ViewEventsByMonth(year, month);
                        break;

                    case 7:
                        Console.Write("Enter the year to view events: ");
                        int viewYear = int.Parse(Console.ReadLine());
                        calendar.ViewEventsByYear(viewYear);
                        break;

                    case 8:
                        calendar.ViewAllEvents();
                        break;

                    case 9:
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
}
